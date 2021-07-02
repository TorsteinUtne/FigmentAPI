using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PowerService.Controllers.API;
using PowerService.DAL.Context.RequestResponseObjects;
using PowerService.Data;
using PowerService.Data.Models;
using PowerService.Data.Models.Queries;
using PowerService.Data.Models.RequestResponseObjects;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;
using PowerService.Services;
using PowerService.Util;

namespace PowerService.DAL.Context
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly PowerServiceContext _context;
            private readonly IUriService _uriService;
            private readonly ILogger _logger;

            public ActivitiesController(PowerServiceContext context, IUriService uriService, ILogger<ActivitiesController> logger)
            {
                _context = context;
                _uriService = uriService;
                _logger = logger;
            }



            /// <summary>
            /// GetActivities henter ut aktivititerer, basert på et eller flere søkeparametre. Maks antall oppføringer per side er 250 (kan endres i config). For å søke på flere søkefelt samtidig, bruk semikolon for å skille søkefelt. Feilstavede søkefelt og sorteringsfelt blir ignorert
            /// </summary>
            /// <param name="searchParameters">Objekt som inneholder søkefelt, søkeverdi, paginering, maks antall records, sorteringsfelt, samt sorteringsrekkefølge </param>
            /// <returns></returns>
            [Authorize]
            [Microsoft.AspNetCore.Mvc.HttpGet]
            [ApiConventionMethod(typeof(DefaultApiConventions),
                nameof(DefaultApiConventions.Get))]

            public async Task<ActionResult<PagedResponse<List<ActivityResponse>>>> GetActivities(
                [FromQuery] Data.Models.Queries.SearchParameter searchParameters)
            {
                var route = Request.Path.Value;

                try
                {
                    var activities = await BuildAndExecuteQuery(searchParameters);
                    var results = await BuildResponseObjects(activities.Results);
                    var pagedResponse = CreatePagedResponses(searchParameters, results, activities.TotalRecords, route);

                    HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, "", 10001, Request.Path.Value);
                    return Ok(pagedResponse);
                }
                catch (UnauthorizedAccessException uae)
                {
                    HelpFunctions.CreateLogEntry(LogLevel.Warning, _logger, uae, "", 30001, Request.Path.Value);
                    return Unauthorized();
                }
                catch (Exception ex)
                {
                    HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, "", 59001, Request.Path.Value);
                    return BadRequest(ex.Message);
                }
            }
            ///// <summary>
            ///// Retrieves a single activity
            ///// </summary>
            ///// <param name="id">GUID for the activity</param>
            ///// <returns></returns>
            //// GET: api/activities/{GUID}
            [Authorize]
            [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
            [ApiConventionMethod(typeof(DefaultApiConventions),
                         nameof(DefaultApiConventions.Get))]

            public async Task<ActionResult<Response<ActivityResponse>>> GetActivity(Guid id)
            {
                try
                {
                    var retrievedActivity = await new ActivityResponse().GetResponse(id, _context);

                    if (retrievedActivity == null)
                    {
                        return NotFound();
                    }
                    HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, $"Successfully retrieved activity with Id {id}", 10001, Request.Path.Value);
                    var response = new Response<ActivityResponse>
                    {
                        Data = retrievedActivity.Value,
                        Message = $"Successfully retrieved activity with Id {id}",
                        Succeeded = true,
                        Errors = null
                    };


                    return Ok(response);
                }
                catch (Exception ex)
                {
                    HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, null, ex.Message, 59001, Request.Path.Value);
                    return BadRequest(ex.Message);
                }
            }
            ////PATCHE eier - evt egen assign - funksjon
            ////TODO: SJekk for endring i eier
            ////Forsøk på endring av readonly - attributter må resultere i feilmelding - sånn at det ikke er mulig å patche id eller createdat, osv
            ////TODO: Patch og Post av accounttype må valideres
            ///// <summary>
            ///// 
            ///// </summary>
            ///// <param name="id">Guid to the address that is to be patched</param>
            ///// <param name="patch">JSON Patch document</param>
            ///// <returns></returns>
            [Authorize]
            [Microsoft.AspNetCore.Mvc.HttpPatch("{id}")]
            public async Task<ActionResult<Response<ActivityResponse>>> PatchAccount([FromUri] Guid id, [Microsoft.AspNetCore.Mvc.FromBody] JsonPatchDocument<ActivityRequest> patch)
            {
                var activity = await _context.Activities.FindAsync(id);
                if (activity == null)
                    return NotFound();
                var activityRequest = await new ActivityRequest().GetRequest(activity.Id, _context); //Henter frem en komplett request slik at hele objektet kan returnerens
                try
                {
                    patch.Sanitize();
                }
                catch (Exception ex)
                {
                    HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, null, patch.ToString(), 59001, Request.Path.Value);
                    return BadRequest(ex.Message);
                }
                patch.ApplyTo(activityRequest.Value, ModelState);
                var updatedActivity = new Activity(activityRequest.Value, activity.OwnerId.Value, _context); //Kopierer inn nye verdier
                                                                        //Merger dem over på eksisterende account
                MappingFunctions.CopyValues<Activity>(activity, updatedActivity);
                _context.Entry(activity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, patch.ToString(), 10002, Request.Path.Value);
                var response = new ActivityResponse().GeneratePatchResponse(patch, updatedActivity, Request.Path.Value, _context);
                return Ok(response);
            }




        ///// <summary>
        ///// Creates a new activity
        ///// </summary>
        ///// <param name="activityRequest">Request-object of type activityRequest to create a new address</param>
        ///// <returns></returns>
        //// POST: api/Activities
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
            [Microsoft.AspNetCore.Mvc.HttpPost]
            [ApiConventionMethod(typeof(DefaultApiConventions),
                         nameof(DefaultApiConventions.Post))]
            public async Task<ActionResult<Response<ActivityResponse>>> PostActivity([Microsoft.AspNetCore.Mvc.FromBody] ActivityRequest activityRequest)
            {
                try
                {
                    Claim first = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    if (first != null)
                    {
                        string userId = first.Value;
                        var owner = await _context.PortalUsers.FirstOrDefaultAsync(i => i.AuthOId == userId);
                        var activity = new Activity(activityRequest, owner.Id, _context);

                        _context.Activities.Add(activity);
                        await _context.SaveChangesAsync();
                        HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, "Created activity with ID: " + activity.Id, 10003, Request.Path.Value);
                        var response = new Response<ActivityResponse>
                        {
                            Data = new ActivityResponse().GetResponse(activity.Id, _context).Result.Value,
                            Errors = null,
                            Message = $"Created activity with ID: {activity.Id}",
                            Succeeded = true
                        };
                        return Ok(response);

                    }

                    return Unauthorized();
                }
                catch (Exception ex)
                {
                    HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                    return BadRequest(ex.Message);
                }
            }
            ///// <summary>
            ///// Deletes a single activity
            ///// </summary>
            ///// <param name="id">Guid to the activity that is to be deleted</param>
            ///// <returns></returns>
            [Authorize]
            // DELETE: api/Activity/{Guid}
            [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
            [ApiConventionMethod(typeof(DefaultApiConventions),
                nameof(DefaultApiConventions.Delete))]
            public async Task<ActionResult<Response<string>>> DeleteActivity(Guid id)
            {
                try
                {
                    var activity = await _context.Activities.FindAsync(id);
                    if (activity == null)
                    {
                        return NotFound();
                    }

                    _context.Activities.Remove(activity);
                    await _context.SaveChangesAsync();
                    HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null,
                        $"Record with Id {id.ToString()} was deleted", 10004, Request.Path.Value);
                    var response = new Response<string>
                    {
                        Message = $"Activity record was deleted",
                        Data = $"ID: {id.ToString()}",
                        Succeeded = true
                    };
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                    return BadRequest(ex.Message);
                }
            }
        /// <summary>
        /// Retrieves all attachments for an activity
        /// </summary>
        /// <param name="id">The Id for the Activity</param>
        /// <returns></returns>
        [Authorize]

        [Microsoft.AspNetCore.Mvc.HttpGet("attachments/{id}")]
            [ApiConventionMethod(typeof(DefaultApiConventions),
                nameof(DefaultApiConventions.Get))]

            public async Task<ActionResult<Response<List<AttachmentResponse>>>> GetAttachments(Guid id)
            {
                try
                {

                    var attachments = await new AttachmentResponse().GetResponses(id, _context);

                    if (attachments == null)
                    {
                        return NotFound();
                    }
                    HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, $"Successfully retrieved activity with Id {id}", 10001, Request.Path.Value);
                    var response = new Response<List<AttachmentResponse>>
                    {
                        Data = attachments.Value,
                        Message = $"Successfully retrieved attachments for activity with Id {id}",
                        Succeeded = true,
                        Errors = null
                    };
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, null, ex.Message, 59001, Request.Path.Value);
                    return BadRequest(ex.Message);
                }
            }
        /// <summary>
        /// This endpoints add a document to an activity as an attachment. To use it you need to have uploaded a document and know the document-ID of that document as well as the activityID
        /// </summary>
        /// <param name="attachmentRequest"></param>
        /// <returns></returns>

        [Authorize]

        [Microsoft.AspNetCore.Mvc.HttpPost("attachments/")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Response<AttachmentResponse>>> AddAttachments([Microsoft.AspNetCore.Mvc.FromBody] AttachmentRequest attachmentRequest)
        {
            try
            {

                var attachment = await _context.Documents.FindAsync(attachmentRequest.DocumentId);

                attachment.ActivityId = attachmentRequest.ActivityId;
                await _context.SaveChangesAsync();
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, $"Attached document with ID: {attachmentRequest.DocumentId} to activity with ID: {attachmentRequest.ActivityId}", 10003, Request.Path.Value);
                var responseData = new AttachmentResponse().GetResponse(attachment.ActivityId.Value, _context).Result.Value;
                var response = new Response<AttachmentResponse>
                {
                    Data = responseData,
                    Errors = null,
                    Message = $"Attached document with ID: {attachmentRequest.DocumentId} to activity with ID: {attachmentRequest.ActivityId}",
                    Succeeded = true
                };
                return Ok(response);


            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }
        ///// <summary>
        ///// Deletes the attachment. The connection between the activity and the document is removed, but both records are kept. To completely delete the document use the Documents endpoint and pass the document ID after this operation is completed.
        ///// </summary>
        ///// <param name="id">Guid to the document that is to be removed</param>
        ///// <returns></returns>
        [Authorize]
        // DELETE: api/Activity/{Guid}

        [Microsoft.AspNetCore.Mvc.HttpDelete("attachments/{id}")]
            [ApiConventionMethod(typeof(DefaultApiConventions),
                         nameof(DefaultApiConventions.Delete))]
            public async Task<ActionResult<Response<string>>> RemoveAttachment(Guid id)
            {
                try
                {
                    var document = await _context.Documents.FindAsync(id);
                    if (document == null)
                    {
                        return NotFound();
                    }

                    document.ActivityId = null;
                   
                    await _context.SaveChangesAsync();
                    HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null,
                        $"Document with Id {id.ToString()} was removed from the activity", 10004, Request.Path.Value);
                    var response = new Response<string>
                    {
                        Message = $"Attachment was removed",
                        Data = $"Document ID: {id.ToString()}",
                        Succeeded = true
                    };
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                    return BadRequest(ex.Message);
                }
            }
        /// <summary>
        /// Retrieves all relations for an activity. These are also included in the GETActivity, but this way they can be retrieved with significantly less payload
        /// </summary>
        /// <param name="id">The Id for the Activity</param>
        /// <returns></returns>
        [Authorize]

        [Microsoft.AspNetCore.Mvc.HttpGet("relations/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]

        public async Task<ActionResult<Response<List<RelationResponse>>>> GetRelations(Guid id)
        {
            try
            {

                var activity = await _context.Activities.FindAsync(id);
                if (activity == null)
                {
                    return NotFound();
                }

                var relatedObjects = new ActivityResponse().GetResponse(id, _context).Result.Value.RelatedObjects;
               
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, $"Successfully retrieved activity with Id {id}", 10001, Request.Path.Value);
                var data = (from relatedObject in relatedObjects where relatedObject.ForeignKeyValue != null select new RelationResponse {ActivityId = id, ForeignKeyName = relatedObject.ForeignKeyName, ForeignKeyId = relatedObject.ForeignKeyValue.Value}).ToList();

                var response = new Response<List<RelationResponse>>
                {
                    Data = data,
                    Message = $"Successfully retrieved relations for activity with Id {id}",
                    Succeeded = true,
                    Errors = null
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, null, ex.Message, 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPost("relations/")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Response<RelationResponse>>> AddRelation([Microsoft.AspNetCore.Mvc.FromBody] RelationRequest relationRequest)
        {
            try
            {

                var activity = await _context.Activities.FindAsync(relationRequest.ActivityId);

                var relatedObject = new RelatedObjects
                {
                    ForeignKeyName = relationRequest.ForeignKeyName,
                    ForeignKeyValue = relationRequest.ForeignKeyId
                };
                MappingFunctions.AddRelationToActivity(activity, relatedObject);
                await _context.SaveChangesAsync();
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, "Created activity with ID: " + activity.Id, 10003, Request.Path.Value);
                var relationResponse = new RelationResponse { ActivityId = relationRequest.ActivityId, ForeignKeyName = relatedObject.ForeignKeyName, ForeignKeyId = relatedObject.ForeignKeyValue.Value };
                var response = new Response<RelationResponse>
                {
                    Data = relationResponse,
                    Errors = null,
                    Message = $"Added relation from activity with ID: {activity.Id} to {relationRequest.ForeignKeyName} with ID: {relationRequest.ForeignKeyId}",
                    Succeeded = true
                };
                return Ok(response);


            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }
        ///// <summary>
        ///// Deletes a single activity
        ///// </summary>
        ///// <param name="id">Guid to the activity that is to be deleted</param>
        ///// <returns></returns>
        [Authorize]
        // DELETE: api/Activity/{Guid}
        [Microsoft.AspNetCore.Mvc.HttpDelete("relations/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                         nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<Response<string>>> RemoveRelation(Guid id, Guid foreignKeyId)
        {
            try
            {


                var activity = await _context.Activities.FindAsync(id);
                if (activity == null)
                {
                    return NotFound();
                }

                var relatedObjects = new ActivityResponse().GetResponse(id, _context).Result.Value.RelatedObjects;
                //Fjerner den oppføringen i listen over relasjoner hvor foreignkeyid er lik foreignKeuid
                string foreignKeyName = "";
                foreach (RelatedObjects relObject in relatedObjects)
                {
                    if (relObject.ForeignKeyValue == foreignKeyId)
                    {
                        foreignKeyName = relObject.ForeignKeyName;
                        break;
                    }
                }

                MappingFunctions.RemoveRelations(activity,  foreignKeyName);
                await _context.SaveChangesAsync();

                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null,
                    $"Relation between activity with Id {id.ToString()} and {foreignKeyName} with ID: {foreignKeyId} was removed", 10004, Request.Path.Value);
                var response = new Response<string>
                {
                    Message = $"Activity record was deleted",
                    Data = $"ID: {id.ToString()}",
                    Succeeded = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }

        #region Functions
        private async Task<QueryResults<List<Activity>>> BuildAndExecuteQuery(SearchParameter searchParameters)
            {
                var searchText = "";

                var columns = MappingFunctions.GetDisplayableColumns<Data.Models.RequestResponseObjects.ActivityRequest>();
                var orderClause = MappingFunctions.BuildOrderClause(searchParameters, columns, "Name");
                var searchFields = searchParameters.SearchField.Split(';').ToList(); //Splitter mulige flere søkefelt til en array
                searchText = MappingFunctions.BuildSearchClause(searchParameters, columns.Contains(searchParameters.SearchField) ? searchFields : columns, _context);
                var totalRecords = await _context.Activities
                    .Where(searchText, StringComparison.InvariantCultureIgnoreCase).CountAsync();
                var activities = await _context.Activities
                    .Where(searchText, StringComparison.InvariantCultureIgnoreCase).OrderBy(orderClause)
                    .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                    .Take(searchParameters.PageSize).ToListAsync();
                return new QueryResults<List<Activity>>
                {
                    Results = activities,
                    TotalRecords = totalRecords
                };
            }
            private PagedResponse<List<ActivityResponse>> CreatePagedResponses(SearchParameter searchParameters,
                List<ActivityResponse> results, int totalRecords,
                string route)
            {
                var pagedResponse = PaginationHelper.CreatePagedReponse<ActivityResponse>(results,
                    new PaginationFilter(searchParameters.PageNumber, searchParameters.PageSize), totalRecords, _uriService,
                    route);
                return pagedResponse;
            }

            private async Task<List<ActivityResponse>> BuildResponseObjects(List<Activity> activities)
            {
                var results = new List<ActivityResponse>();
                foreach (var activity in activities)
                {
                    var response = await new ActivityResponse().GetResponse(activity.Id, _context);
                    results.Add(response.Value);
                }

                return results;
            }
            #endregion
        }
    }
