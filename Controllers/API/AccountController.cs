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

        //Er ikke dette egentlig GET - søk er vel default?
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                    nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAccounts([FromQuery] Data.Models.Queries.SearchParameter searchParameters)
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
                    var columns = MappingFunctions.GetDisplayableColumns<Account>();
                    int i = 0;
                    foreach (var column in columns )
                    {
                        searchText += column + ".Contains(\"" + searchParameters.SearchValue + "\")";
                        i++;
                        if( columns.Count > i)
                            searchText += " || ";
                    }
                    if(orderClause != string.Empty)
                        accounts = await _context.Accounts.Where(searchText).OrderBy(orderClause).ToListAsync();
                   else
                        accounts = await _context.Accounts.Where(searchText).ToListAsync();
                }
                else
                {
                    if (orderClause != string.Empty)
                        accounts = await _context.Accounts.Where(searchParameters.SearchField + ".Contains(@0)", searchParameters.SearchValue).OrderBy(orderClause).ToListAsync();
                    else
                        accounts = await _context.Accounts.Where(searchParameters.SearchField + ".Contains(@0)", searchParameters.SearchValue).ToListAsync();
                }

                //Convert list to AccountModel
                var results = new List<AccountModel>();
                foreach(var account in accounts)
                {
                    results.Add(new AccountModel(account, _context));
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
     
        public async Task<ActionResult<AccountModel>> GetAccount(Guid id)
        {
            try
            {
                var accountEntity = await _context.Accounts.FindAsync(id);

                if (accountEntity == null)
                {
                    return NotFound();
                }

                return Ok(new AccountModel(accountEntity, _context));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //PATCHE eier - evt egen assign - funksjon
        //TODO: SJekk for endring i eier
        //Forsøk på endring av readonly - attributter må resultere i feilmelding - sånn at det ikke er mulig å patche id eller createdat, osv
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPatch("{id}")]
       public async Task<IActionResult> PatchAccount([FromUri]Guid id, [FromBody]JsonPatchDocument<AccountModel>  patch)
        {
            var account = await _context.Accounts.FindAsync(id);
            var accountModel = new AccountModel(account, _context);
            patch.Sanitize<AccountModel>();
            patch.ApplyTo(accountModel, ModelState);
            var updatedAccount = new Account(accountModel, account.OwnerId.Value); //Copies to an account object to merge new values
            //    _context.Accounts.Update(updatedAccount);
            //Merge the changes into the currentAccount
            MappingFunctions.CopyValues<Account>(account, updatedAccount);
            _context.Entry(account).State = EntityState.Modified;
          
            await _context.SaveChangesAsync();
            return Ok(accountModel);
        }

       

        // POST: api/Account
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<AccountModel>> PostAccount([FromBody]AccountModel accountModel)
        {
            try
            {
               
                //Set defaults
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

               //
               var owner = await _context.PortalUsers.FirstOrDefaultAsync(i => i.AuthOId == userId);
                var account = new Account(accountModel, owner.Id);

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                return CreatedAtAction("POST", new { id = account.Id }, new AccountModel(account, _context)); 
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
                return Ok(new AccountModel(account, _context));
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