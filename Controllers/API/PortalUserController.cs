

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerService.Data;
using PowerService.Data.Models;

namespace PowerService.DAL.Context
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortalUserController : ControllerBase
    {
        private readonly PowerServiceContext _context;

        public PortalUserController(PowerServiceContext context)
        {
            _context = context;
        }
        // GET: api/PortalUser
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortalUser>>> GetPortalUsers()
        {
            return await _context.PortalUsers.ToListAsync();
        }

        // GET: api/PortalUser/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PortalUser>> GetPortalUser(Guid id)
        {
            var portalUser = await _context.PortalUsers.FindAsync(id);

            if (portalUser == null)
            {
                return NotFound();
            }

            return portalUser;
        }

        // PUT: api/PortalUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortalUser(Guid id, PortalUser portalUser)
        {
            if (id != portalUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(portalUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortalUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Document
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PortalUser>> PostPortalUser(PortalUser portalUser)
        {
            _context.PortalUsers.Add(portalUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPortalUser", new { id = portalUser.Id }, portalUser);
        }

        // DELETE: api/PortalUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PortalUser>> DeletePortalUser(Guid id)
        {
            var portalUser = await _context.PortalUsers.FindAsync(id);
            if (portalUser == null)
            {
                return NotFound();
            }

            _context.PortalUsers.Remove(portalUser);
            await _context.SaveChangesAsync();

            return portalUser;
        }

        private bool PortalUserExists(Guid id)
        {
            return _context.PortalUsers.Any(e => e.Id == id);
        }
    }
}

