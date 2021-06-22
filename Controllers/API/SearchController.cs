using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerService.Data;
using PowerService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PowerService.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly PowerServiceContext _context;

        public SearchController(PowerServiceContext context)
        {
            _context = context;
        }

        //[Authorize]
        //[Microsoft.AspNetCore.Mvc.HttpGet]
        //[ApiConventionMethod(typeof(DefaultApiConventions),
        //             nameof(DefaultApiConventions.Get))]
        //public async Task<ActionResult<IEnumerable<IRecord>>> Search([FromQuery] Data.Models.Queries.SearchParameter searchParameters)
        //{
        //    var entityCollections = _context.Model.GetEntityTypes().OrderBy(x => x.Name).ToList();

        //    var entities = new Dictionary<string, Func<DbContext, IQueryable>>()
        //    //{
        //    //    { "Accounts", ( DbContext context ) => context.Set<Account>() }
        //    //};
        //    foreach (var entityType in entityCollections)
        //    {
        //        entities.Add(entityType.Name, (_context => _context.Set<Account>()));
        //    };
        //    DbSet<Account> dbSet = c


       
         
        //    //Extract parameters
        //    int pageNumber = searchParameters.PageNumber;
        //    int pageSize = searchParameters.PageSize;
          
        //    string searchValue = searchParameters.SearchValue;
        //    string sortingField = searchParameters.SortingField;
        //    string order = searchParameters.Order;

        //    try
        //    {


        //        var result = await _context.Accounts
        //       .OrderBy(x => x.Name) //TODO: Change to queryParamenters.Sortingfield
        //       .Skip((pageNumber - 1) * pageSize)
        //       .Take(pageSize)
        //       .ToListAsync();
        //        return Ok(result);

        //    }
        //    catch (UnauthorizedAccessException)
        //    {
        //        return Unauthorized();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}
    }
}
