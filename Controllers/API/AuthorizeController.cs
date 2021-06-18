using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Controllers.API
{
    //Controller to Authorize access for the session using Auth0
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly PowerServiceContext _context;
        public AuthorizeController(PowerServiceContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<object>> Login(string username, string password)
        {
            return Ok();
        }

    }
}
