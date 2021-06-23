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

namespace PowerService.DAL.Context
{
    //TODO: Kontroller for alle synlige felt på Accountmodel - til bruk i grensesnittet  legg det inn som en egen modelcontroller
    //TODO:  sjekke delete, legge til søk
    //Legge til tilleggsattrributer
    //implementere audit
    //opprettet, endret, sporing, logging
    //Sjekke om autentiseringen faktisk fungerer
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
  
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly PowerServiceContext _context;
        
        public AccountController(PowerServiceContext context)
        {
            _context = context;
        }
        //TODO : Implement request -response class instead of custom types, using path only
        // See https://mattfrear.com/2020/04/21/request-and-response-examples-in-swashbuckle-aspnetcore/
        //En request-klasse for å spørre om data
        //en responsklasse for å levere resultatet, skreddersydd til request
        //underliggende modell som er knyttet til databasen
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                    nameof(DefaultApiConventions.Get))]
       //TODO: FIltrer på Owner-feltet
        public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccounts([FromQuery] Data.Models.Queries.SearchParameter searchParameters)
        {
            string orderClause = "";
            if (searchParameters.SortingField == string.Empty)
                searchParameters.SortingField = "Name";
            if (searchParameters.Order.ToLower() == "desc")
                {
                    orderClause = searchParameters.SortingField + " DESC";
                }
                else
                {
                    orderClause = searchParameters.SortingField + " ASC";
                }
         
            try
            {
                var accounts = new List<Account>();
                if (string.IsNullOrEmpty(searchParameters.SearchField))
                {
                    string searchText = "";
                    var columns = MappingFunctions.GetDisplayableColumns<AccountRequest>();
                    int i = 0;
                    foreach (var column in columns )
                    {
                        searchText += column + ".Contains(\"" + searchParameters.SearchValue + "\")";
                        i++;
                        if( columns.Count > i)
                            searchText += " || ";
                    }
                    if(orderClause != string.Empty)
                        accounts = await _context.Accounts.Where(searchText, StringComparison.InvariantCultureIgnoreCase).OrderBy(orderClause).ToListAsync();
                   else
                        accounts = await _context.Accounts.Where(searchText, StringComparison.InvariantCultureIgnoreCase).ToListAsync();
                }
                else
                {
                    if (orderClause != string.Empty)
                        accounts = await _context.Accounts.Where(searchParameters.SearchField + ".Contains(@0)", searchParameters.SearchValue, StringComparison.InvariantCultureIgnoreCase).OrderBy(orderClause).ToListAsync();
                    else
                        accounts = await _context.Accounts.Where(searchParameters.SearchField + ".Contains(@0)", searchParameters.SearchValue, StringComparison.InvariantCultureIgnoreCase).ToListAsync();
                }

                //Convert list to AccountModel
                var results = new List<AccountResponse>();
                foreach(var account in accounts)
                {
                    var response = await new AccountResponse().GetResponse(account.Id, _context);
                    results.Add(response.Value);
                }
                return Ok(results);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
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

                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //PATCHE eier - evt egen assign - funksjon
        //TODO: SJekk for endring i eier
        //Forsøk på endring av readonly - attributter må resultere i feilmelding - sånn at det ikke er mulig å patche id eller createdat, osv
        //TODO: Patch og Post av accounttype må valideres
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPatch("{id}")]
       public async Task<IActionResult> PatchAccount([FromUri]Guid id, [FromBody]JsonPatchDocument<AccountRequest>  patch)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();
            var accountRequest = await new AccountRequest().GetRequest(account.Id, _context); //Henter frem en komplett request slik at hele objektet kan returnerens
            string logMessage = "";
            try
            {
                patch.Sanitize<AccountRequest>(out logMessage);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            patch.ApplyTo(accountRequest.Value, ModelState);
            var updatedAccount = new Account(accountRequest.Value, account.OwnerId.Value); //Kopierer inn nye verdier
            //Merger dem over på eksisterende account
            MappingFunctions.CopyValues<Account>(account, updatedAccount);
            _context.Entry(account).State = EntityState.Modified;
          
            await _context.SaveChangesAsync();
            return Ok(accountRequest.Value);
        }

       

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
               
                //Set defaults
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

               //
               var owner = await _context.PortalUsers.FirstOrDefaultAsync(i => i.AuthOId == userId);
                var account = new Account(accountRequest, owner.Id);

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                return CreatedAtAction("POST", new { id = account.Id }, new AccountResponse().GetResponse(account.Id, _context).Result.Value); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<AccountModel>> DeleteAccount(Guid id)
        {try
            {
                var account = await _context.Accounts.FindAsync(id);
                if (account == null)
                {
                    return NotFound();
                }
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return Ok(String.Format("Record with Id {0} was deleted", id.ToString()));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        private bool AccountExists(Guid id)
        {
            try
            {
                return _context.Accounts.Any(e => e.Id == id);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}