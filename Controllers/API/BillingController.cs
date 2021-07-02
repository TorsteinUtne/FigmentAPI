using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class BillingController : ControllerBase
    {
        private readonly PowerServiceContext _context;
        private readonly IUriService _uriService;
        private readonly ILogger _logger;
        public BillingController(PowerServiceContext context, IUriService uriService, ILogger<BillingController> logger)
        {
            _context = context;
            _uriService = uriService;
            _logger = logger;
        }

        /// <summary>
        /// Get Billings henter ut fakturainformasjon, basert på et eller flere søkeparametre. Maks antall oppføringer per side er 250 (kan endres i config). For å søke på flere søkefelt samtidig, bruk semikolon for å skille søkefelt. Feilstavede søkefelt og sorteringsfelt blir ignorert
        /// </summary>
        /// <param name="searchParameters">Objekt som inneholder søkefelt, søkeverdi, paginering, maks antall records, sorteringsfelt, samt sorteringsrekkefølge </param>
        /// <returns></returns>
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                    nameof(DefaultApiConventions.Get))]
        //TODO: FIltrer på Owner-feltet
        public async Task<ActionResult<PagedResponse<List<BillingResponse>>>> GetBillings([FromQuery] Data.Models.Queries.SearchParameter searchParameters)
        {
            //TODO: Refactor, build service repository for methods accessing all entity types
            var route = Request.Path.Value;


            try
            {
                var billings = await BuildAndExecuteQuery(searchParameters);
                var results = await BuildResponseObjects(billings.Results);
                //Denne bør gi tall på antall treff, ikke antall oppføringer totalt. results-objektet er begrenset av skip-take, og vil ikke vise dette.
                var pagedResponse = CreatePagedResponses(searchParameters, results, billings.TotalRecords, route);

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

        private PagedResponse<List<BillingResponse>> CreatePagedResponses(SearchParameter searchParameters, List<BillingResponse> results, int totalRecords,
            string route)
        {
            var pagedResponse = PaginationHelper.CreatePagedReponse<BillingResponse>(results,
                new PaginationFilter(searchParameters.PageNumber, searchParameters.PageSize), totalRecords, _uriService, route);
            return pagedResponse;
        }

        private async Task<List<BillingResponse>> BuildResponseObjects(List<Billing> billings)
        {
            var results = new List<BillingResponse>();
            foreach (var billing in billings)
            {
                var response = await new BillingResponse().GetResponse(billing.Id, _context);
                results.Add(response.Value);
            }

            return results;
        }






        /// <summary>
        /// Retrieves a single billing
        /// </summary>
        /// <param name="id">GUID for the billing</param>
        /// <returns></returns>
        // GET: api/Account/5
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Get))]

        public async Task<ActionResult<Response<BillingResponse>>> GetBilling(Guid id)
        {
            try
            {
                var retrievedBilling = await new BillingResponse().GetResponse(id, _context);

                if (retrievedBilling == null)
                {
                    return NotFound();
                }
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, $"Successfully retrieved billing with Id {id}", 10001, Request.Path.Value);
                var response = new Response<BillingResponse>
                {
                    Data = retrievedBilling.Value,
                    Message = $"Successfully retrieved billing with Id {id}",
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
        //PATCHE eier - evt egen assign - funksjon
        //TODO: SJekk for endring i eier
        //Forsøk på endring av readonly - attributter må resultere i feilmelding - sånn at det ikke er mulig å patche id eller createdat, osv
        /// <summary>
        /// oppdaterer en billing - ikke alle oppføringer kan endres her!
        /// </summary>
        /// <param name="id">Guid to the billing that is to be patched</param>
        /// <param name="patch">JSON Patch document</param>
        /// <returns></returns>
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPatch("{id}")]
        public async Task<ActionResult<Response<BillingResponse>>> PatchBilling([FromUri] Guid id, [Microsoft.AspNetCore.Mvc.FromBody] JsonPatchDocument<BillingRequest> patch)
        {
            var billing = await _context.Billings.FindAsync(id);
            if (billing == null)
                return NotFound();
            var billingRequest = await new BillingRequest().GetRequest(billing.Id, _context); //Henter frem en komplett request slik at hele objektet kan returnerens
            try
            {
                patch.Sanitize();
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, null, patch.ToString(), 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
            patch.ApplyTo(billingRequest.Value, ModelState);
            var updatedBilling = new Billing(billingRequest.Value, billing.OwnerId.Value, _context); //Kopierer inn nye verdier
            //Merger dem over på eksisterende account
            MappingFunctions.CopyValues<Billing>(billing, updatedBilling);
            _context.Entry(billing).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, patch.ToString(), 10002, Request.Path.Value);
            var response = new BillingResponse().GeneratePatchResponse(patch, updatedBilling, Request.Path.Value, _context);
            return Ok(response);
        }




        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="billingRequest">Request-object of type AccountRequest to create a new account</param>
        /// <returns></returns>
        // POST: api/Account
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Response<BillingResponse>>> PostBilling([Microsoft.AspNetCore.Mvc.FromBody] BillingRequest billingRequest)
        {
            try
            {
                Claim first = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (first != null)
                {
                    string userId = first.Value;
                    var owner = await _context.PortalUsers.FirstOrDefaultAsync(i => i.AuthOId == userId);
                    var billing = new Billing(billingRequest, owner.Id, _context);

                    _context.Billings.Add(billing);
                    await _context.SaveChangesAsync();
                    HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, "Created billing with ID: " + billing.Id, 10003, Request.Path.Value);
                    var response = new Response<BillingResponse>
                    {
                        Data = new BillingResponse().GetResponse(billing.Id, _context).Result.Value,
                        Errors = null,
                        Message = $"Created billing with ID: {billing.Id}",
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

        //TODO: Check for rules and regulations
        /// <summary>
        /// Deletes a single billing - should only be allowed for admins. Softdelete only? Drafts should be deleted, sent should be credited, ects
        /// </summary>
        /// <param name="id">Guid to the billing that is to be deleted</param>
        /// <returns></returns>
        [Authorize]
        // DELETE: api/Billing/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<Response<string>>> DeleteBilling(Guid id)
        {
            try
            {
                var billing = await _context.Billings.FindAsync(id);
                if (billing == null)
                {
                    return NotFound();
                }

                _context.Billings.Remove(billing);
                await _context.SaveChangesAsync();
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null,
                    $"Record with Id {id.ToString()} was deleted", 10004, Request.Path.Value);
                var response = new Response<string>
                {
                    Message = $"Billing record was deleted",
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
        private async Task<QueryResults<List<Billing>>> BuildAndExecuteQuery(SearchParameter searchParameters)
        {
            var searchText = "";

            var columns = MappingFunctions.GetDisplayableColumns<BillingRequest>();
            var orderClause = MappingFunctions.BuildOrderClause(searchParameters, columns, "Name");
            var searchFields = searchParameters.SearchField.Split(';').ToList();


            //Splitter mulige flere søkefelt til en array
            if (columns.Contains(searchParameters.SearchField)) //Ellers søker vi på alle synlige kolonner
            {
                //Sjekker om searchField er i listen av displayable columns - da setter vi searchtext til den, hvis ikke bygger vi en søkestren av alle synlige kolonne
                searchText = MappingFunctions.BuildSearchClause(searchParameters, searchFields, _context);
            }
            else
            {
                searchText = MappingFunctions.BuildSearchClause(searchParameters, columns, _context);

            }

            var totalRecords = await _context.Billings
                .Where(searchText, StringComparison.InvariantCultureIgnoreCase).CountAsync();
            var billings = await _context.Billings
                .Where(searchText, StringComparison.InvariantCultureIgnoreCase).OrderBy(orderClause)
                .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize).ToListAsync();
            return new QueryResults<List<Billing>>
            {
                Results = billings,
                TotalRecords = totalRecords
            };

        }
        #endregion
    }
}