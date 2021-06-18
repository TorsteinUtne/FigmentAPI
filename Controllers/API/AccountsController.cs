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
using PowerService.Data.Models.ModelBinders;
using PowerService.Util;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace PowerService.DAL.Context
{

    //TODO: Rydde opp/Fjerne put, sjekke delete, legge til søk
    //Legge til tilleggsattrributer
    //implementere audit
    //opprettet, endret, sporing, logging
    //Sjekke om autentiseringen faktisk fungerer
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
  
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly PowerServiceContext _context;
        
        public AccountsController(PowerServiceContext context)
        {
            _context = context;
        }

        //TODO: Paginering
        //TODO: Søk
        // GET: api/Account
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAccount()
        {
            try
            {
                var result = await _context.Accounts.ToListAsync();
                return CreatedAtAction(nameof(GetAccount), result);
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

                return new AccountModel(accountEntity, _context);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //TODO: SJekk for endring i eier
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPatch("{id}")]
       public async Task<IActionResult> PatchAccount([FromUri]Guid id, [FromBody]JsonPatchDocument<AccountModelUpdate>  patch)
        {
            var account = await _context.Accounts.FindAsync(id);
            var accountModel = new AccountModelUpdate(account, _context);
            patch.ApplyTo(accountModel, ModelState);
            var updatedAccount = new Account(accountModel, account.OwnerId.Value); //Copies to an account object to merge new values
            //    _context.Accounts.Update(updatedAccount);
            //Merge the changes into the currentAccount
            MappingFunctions.CopyValues<Account>(account, updatedAccount);
            _context.Entry(account).State = EntityState.Modified;
          
            await _context.SaveChangesAsync();
            return CreatedAtAction("PatchAccount", new { id = accountModel.Id }, accountModel);
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Put))]
  
        public async Task<IActionResult> PutAccount(Guid id, AccountModelUpdate accountModel)
        {
        
            if (accountModel == null)
            {
                BadRequest();
            }
            if (id == Guid.Empty)
            {
                BadRequest();
            }
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var owner = await _context.PortalUsers.FirstOrDefaultAsync(i => i.AuthOId == userId);
            //Get current account for update
            var account = (Account)MappingFunctions.ApplyChangesToRecord(await _context.Accounts.FindAsync(id), accountModel, typeof(Account), typeof(AccountModel));
            //add any changed fields from the model
            
            account.OwnerId = Util.HelpFunctions.GetCurrentUserId();
            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("PutAccount", new { id = accountModel.Id }, new AccountModel(account, _context)); //TODO: Sjekk at det er mulig å bytte eier med denne metoden, ellers må det en egen assign-metode til og
            }
            catch (DbUpdateConcurrencyException dce)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(dce.Message);
                }
            }
        }

        // POST: api/Account
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<AccountModel>> PostAccount(AccountModel accountModel)
        {
            try
            {
               
                //Set defaults
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                //  account.OrganizationId = Util.HelpFunctions.GetOrganizationId();
               // account.OwnerId = await
               //
               var owner = await _context.PortalUsers.FirstOrDefaultAsync(i => i.AuthOId == userId);
                var account = new Account(accountModel, owner.Id);

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                return CreatedAtAction("PostAccount", new { id = account.Id }, account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Guid? GetOwnerIdFromUserIdentity(string userIdentity)
        {
            return Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa8");
        }
        [Authorize]
        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<Account>> DeleteAccount(Guid id)
        {try
            {
                var account = await _context.Accounts.FindAsync(id);
                if (account == null)
                {
                    return NotFound();
                }
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Delete", new { id = account.Id }, account);
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