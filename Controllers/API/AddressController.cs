using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PowerService.DAL.Context;
using PowerService.Data;
using PowerService.Data.Models;
using PowerService.Data.Models.Queries;
using PowerService.Data.Models.RequestResponseObjects;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;
using PowerService.Services;
using PowerService.Util;

namespace PowerService.Controllers.API
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly PowerServiceContext _context;
        private readonly IUriService _uriService;
        private readonly ILogger _logger;

        public AddressController(PowerServiceContext context, IUriService uriService, ILogger<AddressController> logger)
        {
            _context = context;
            _uriService = uriService;
            _logger = logger;
        }



        /// <summary>
        /// Get address henter ut registrerte adresser, basert på et eller flere søkeparametre. Maks antall oppføringer per side er 250 (kan endres i config). For å søke på flere søkefelt samtidig, bruk semikolon for å skille søkefelt. Feilstavede søkefelt og sorteringsfelt blir ignorert
        /// </summary>
        /// <param name="searchParameters">Objekt som inneholder søkefelt, søkeverdi, paginering, maks antall records, sorteringsfelt, samt sorteringsrekkefølge </param>
        /// <returns></returns>
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]
 
        public async Task<ActionResult<PagedResponse<List<AddressResponse>>>> GetAddresses(
            [FromQuery] Data.Models.Queries.SearchParameter searchParameters)
        {
            var route = Request.Path.Value;

            try
            {
                var addresses = await BuildAndExecuteQuery(searchParameters);
                var results = await BuildResponseObjects(addresses.Results);
                var pagedResponse = CreatePagedResponses(searchParameters, results, addresses.TotalRecords, route);

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
        ///// Retrieves a single address
        ///// </summary>
        ///// <param name="id">GUID for the address</param>
        ///// <returns></returns>
        //// GET: api/address/{GUID}
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Get))]

        public async Task<ActionResult<Response<AddressResponse>>> GetAddress(Guid id)
        {
            try
            {
                var retrievedAddress = await new AddressResponse().GetResponse(id, _context);

                if (retrievedAddress == null)
                {
                    return NotFound();
                }
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, $"Successfully retrieved address with Id {id}", 10001, Request.Path.Value);
                var response = new Response<AddressResponse>
                {
                    Data = retrievedAddress.Value,
                    Message = $"Successfully retrieved address with Id {id}",
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
        public async Task<ActionResult<Response<AddressResponse>>> PatchAccount([FromUri] Guid id, [Microsoft.AspNetCore.Mvc.FromBody] JsonPatchDocument<AddressRequest> patch)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
                return NotFound();
            var addressRequest = await new AddressRequest().GetRequest(address.Id, _context); //Henter frem en komplett request slik at hele objektet kan returnerens
            try
            {
                patch.Sanitize();
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, null, patch.ToString(), 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
            patch.ApplyTo(addressRequest.Value, ModelState);
            var updatedAddress = new Address(addressRequest.Value); //Kopierer inn nye verdier
            //Merger dem over på eksisterende account
            MappingFunctions.CopyValues<Address>(address, updatedAddress);
            _context.Entry(address).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, patch.ToString(), 10002, Request.Path.Value);
            var response = new AddressResponse().GeneratePatchResponse(patch, updatedAddress, Request.Path.Value, _context);
            return Ok(response);
        }




        ///// <summary>
        ///// Creates a new address
        ///// </summary>
        ///// <param name="addressRequest">Request-object of type AttachmentRequest to create a new address</param>
        ///// <returns></returns>
        //// POST: api/Address
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Response<AddressResponse>>> PostAddress([Microsoft.AspNetCore.Mvc.FromBody] AddressRequest addressRequest)
        {
            try
            {
                Claim first = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (first != null)
                {
                    string userId = first.Value;
                    var owner = await _context.PortalUsers.FirstOrDefaultAsync(i => i.AuthOId == userId);
                    var address = new Address(addressRequest);

                    _context.Addresses.Add(address);
                    await _context.SaveChangesAsync();
                    HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, "Created address with ID: " + address.Id, 10003, Request.Path.Value);
                    var response = new Response<AddressResponse>
                    {
                        Data = new AddressResponse().GetResponse(address.Id, _context).Result.Value,
                        Errors = null,
                        Message = $"Created address with ID: {address.Id}",
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
        ///// Deletes a single address
        ///// </summary>
        ///// <param name="id">Guid to the address that is to be deleted</param>
        ///// <returns></returns>
        [Authorize]
        // DELETE: api/Account/{Guid}
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<Response<string>>> DeleteAddress(Guid id)
        {
            try
            {
                var address = await _context.Addresses.FindAsync(id);
                if (address == null)
                {
                    return NotFound();
                }

                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null,
                    $"Record with Id {id.ToString()} was deleted", 10004, Request.Path.Value);
                var response = new Response<string>
                {
                    Message = $"Address record was deleted",
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
        private async Task<QueryResults<List<Address>>> BuildAndExecuteQuery(SearchParameter searchParameters)
        {
            var searchText = "";

            var columns = MappingFunctions.GetDisplayableColumns<Data.Models.RequestResponseObjects.AddressRequest>();
            var orderClause = MappingFunctions.BuildOrderClause(searchParameters, columns, "Name");
            var searchFields = searchParameters.SearchField.Split(';').ToList(); //Splitter mulige flere søkefelt til en array
            searchText = MappingFunctions.BuildSearchClause(searchParameters, columns.Contains(searchParameters.SearchField) ? searchFields : columns,  _context);
            var totalRecords = await _context.Addresses
                .Where(searchText, StringComparison.InvariantCultureIgnoreCase).CountAsync();
            var addresses = await _context.Addresses
                .Where(searchText, StringComparison.InvariantCultureIgnoreCase).OrderBy(orderClause)
                .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize).ToListAsync();
            return new QueryResults<List<Address>>
            {
                Results =  addresses,
                TotalRecords = totalRecords
            };
        }
        private PagedResponse<List<AddressResponse>> CreatePagedResponses(SearchParameter searchParameters,
            List<AddressResponse> results, int totalRecords,
            string route)
        {
            var pagedResponse = PaginationHelper.CreatePagedReponse<AddressResponse>(results,
                new PaginationFilter(searchParameters.PageNumber, searchParameters.PageSize), totalRecords, _uriService,
                route);
            return pagedResponse;
        }

        private async Task<List<AddressResponse>> BuildResponseObjects(List<Address> addresses)
        {
            var results = new List<AddressResponse>();
            foreach (var address in addresses)
            {
                var response = await new AddressResponse().GetResponse(address.Id, _context);
                results.Add(response.Value);
            }

            return results;
        }
        #endregion
    }
}