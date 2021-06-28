using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerService.Data;
using PowerService.Data.Models;
using PowerService.Data.Models.FriendlyModels;
using PowerService.Util;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using System.Linq.Dynamic.Core;
using Swashbuckle.Swagger.Annotations;
using PowerService.Data.Models.RequestResponseObjects;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;
using PowerService.Services;
using Microsoft.Extensions.Logging;
using PowerService.Data.Models.Queries;

namespace PowerService.DAL.Context
{
    //TODO: Kontroller for alle synlige felt på Accountmodel - til bruk i grensesnittet  legg det inn som en egen modelcontroller

    //Legge til tilleggsattrributer - avgjøre hva som trengs. Evaluere hvor lang tid det tar å legge til ekstra felt av ulik type - int/string/dato bør gå kjapt
    //implementere audit
    //opprettet, endret, sporing, logging
    //Sjekke om autentiseringen faktisk fungerer

    //TODO: Feil når maks antall records er lavere enn pagesizxe
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
  
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly PowerServiceContext _context;
        private readonly IUriService _uriService;
        private readonly ILogger _logger;
        public AccountController(PowerServiceContext context, IUriService uriService, ILogger<AccountController> logger)
        {
            _context = context;
            _uriService = uriService;
            _logger = logger;
        }
 
        /// <summary>
        /// Get accounst henter ut registrerte accounts, basert på et eller flere søkeparametre. Maks antall oppføringer per side er 250 (kan endres i config). For å søke på flere søkefelt samtidig, bruk semikolon for å skille søkefelt. Feilstavede søkefelt og sorteringsfelt blir ignorert
        /// </summary>
        /// <param name="searchParameters">Objekt som inneholder søkefelt, søkeverdi, paginering, maks antall records, sorteringsfelt, samt sorteringsrekkefølge </param>
        /// <returns></returns>
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                    nameof(DefaultApiConventions.Get))]
       //TODO: FIltrer på Owner-feltet
        public async Task<ActionResult<IEnumerable<Response<List<AccountResponse>>>>> GetAccounts([FromQuery] Data.Models.Queries.SearchParameter searchParameters)
        {
            //TODO: Refactor, build service repository for methods accessing all entity types
            var route = Request.Path.Value;
            

            try
            {
                var accounts = await  BuildAndExecuteQuery(searchParameters);
                var results = await BuildResponseObjects(accounts);
                var totalRecords = await _context.Accounts.CountAsync();
                var pagedResponse = CreatePagedResponses(searchParameters, results, totalRecords, route);
                
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null,"",10001, Request.Path.Value);
                return Ok(pagedResponse);
            }
            catch (UnauthorizedAccessException uae)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Warning, _logger, uae,"",30001, Request.Path.Value);
                return Unauthorized();
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, "",59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }

        private PagedResponse<List<AccountResponse>> CreatePagedResponses(SearchParameter searchParameters, List<AccountResponse> results, int totalRecords,
            string route)
        {
            var pagedResponse = PaginationHelper.CreatePagedReponse<AccountResponse>(results,
                new PaginationFilter(searchParameters.PageNumber, searchParameters.PageSize), totalRecords, _uriService, route);
            return pagedResponse;
        }

        private async Task<List<AccountResponse>> BuildResponseObjects(List<Account> accounts)
        {
            var results = new List<AccountResponse>();
            foreach (var account in accounts)
            {
                var response = await new AccountResponse().GetResponse(account.Id, _context);
                results.Add(response.Value);
            }

            return results;
        }

     


        

        /// <summary>
        /// Retrieves a single account
        /// </summary>
        /// <param name="id">GUID for the account</param>
        /// <returns></returns>
        // GET: api/Account/5
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Get))]
     
        public async Task<ActionResult<AccountResponse>> GetAccount(Guid id)
        {
            try
            {
                var response = await new AccountResponse().GetResponse(id, _context);

                if (response == null)
                {
                    return NotFound();
                }
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, "",10001, Request.Path.Value);
                return Ok(response.Value);
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
        //TODO: Patch og Post av accounttype må valideres
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Guid to the account that is to be patched</param>
        /// <param name="patch">JSON Patch document</param>
        /// <returns></returns>
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPatch("{id}")]
       public async Task<IActionResult> PatchAccount([FromUri]Guid id, [FromBody]JsonPatchDocument<AccountRequest>  patch)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();
            var accountRequest = await new AccountRequest().GetRequest(account.Id, _context); //Henter frem en komplett request slik at hele objektet kan returnerens
            try
            {
                patch.Sanitize();
            }
            catch(Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, null, patch.ToString(), 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
            patch.ApplyTo(accountRequest.Value, ModelState);
            var updatedAccount = new Account(accountRequest.Value, account.OwnerId.Value); //Kopierer inn nye verdier
            //Merger dem over på eksisterende account
            MappingFunctions.CopyValues<Account>(account, updatedAccount);
            _context.Entry(account).State = EntityState.Modified;
          
            await _context.SaveChangesAsync();
            HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, patch.ToString(), 10002, Request.Path.Value);
            return Ok(accountRequest.Value);
        }

       
        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="accountRequest">Request-object of type AccountRequest to create a new account</param>
        /// <returns></returns>
        // POST: api/Account
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<AccountResponse>> PostAccount([FromBody] AccountRequest accountRequest)
        {
            try
            {
                Claim first = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                string userId = first.Value;
                var owner = await _context.PortalUsers.FirstOrDefaultAsync(i => i.AuthOId == userId);
                var account = new Account(accountRequest, owner.Id);

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, "Created account with ID: " + account.Id, 10003, Request.Path.Value);
                return CreatedAtAction("POST", new { id = account.Id }, new AccountResponse().GetResponse(account.Id, _context).Result.Value); 
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Deletes a single account
        /// </summary>
        /// <param name="id">Guid to the account that is to be deleted</param>
        /// <returns></returns>
        [Authorize]
        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<string>> DeleteAccount(Guid id)
        {
            try
            {
                var account = await _context.Accounts.FindAsync(id);
                if (account == null)
                {
                    return NotFound();
                }

                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null,
                    $"Record with Id {id.ToString()} was deleted", 10004, Request.Path.Value);
                return Ok($"Record with Id {id.ToString()} was deleted");
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }

        #region Functions
        private async Task<List<Account>> BuildAndExecuteQuery(SearchParameter searchParameters)
        {
            var searchText = "";

            var columns = MappingFunctions.GetDisplayableColumns<AccountRequest>();
            var orderClause = MappingFunctions.BuildOrderClause(searchParameters, columns, "Name");
            var searchFields = searchParameters.SearchField.Split(';').ToList(); //Splitter mulige flere søkefelt til en array
            if (columns.Contains(searchParameters.SearchField)) //Ellers søker vi på alle synlige kolonner
            {
                //Sjekker om searchField er i listen av displayable columns - da setter vi searchtext til den, hvis ikke bygger vi en søkestren av alle synlige kolonne
                searchText = MappingFunctions.BuildSearchClause(searchParameters, searchFields, searchText);
            }
            else
            {
                searchText = MappingFunctions.BuildSearchClause(searchParameters, columns, searchText);

            }
            var accounts = await _context.Accounts
                .Where(searchText, StringComparison.InvariantCultureIgnoreCase).OrderBy(orderClause)
                .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize).ToListAsync();
            return accounts;
        }
        #endregion
    }
}