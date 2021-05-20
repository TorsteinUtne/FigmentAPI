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
    public class AuditController : ControllerBase
    {
        private readonly PowerServiceContext _context;

        public AuditController(PowerServiceContext context)
        {
            _context = context;
        }
        // GET: api/Audit
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audit>>> GetAudits()
        {
            return await _context.Audits.ToListAsync();
        }

        // GET: api/Audit/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Audit>> GetAudit(Guid id)
        {
            var audits = await _context.Audits.FindAsync(id);

            if (audits == null)
            {
                return NotFound();
            }

            return audits;
        }

        // PUT: api/Audit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAudit(Guid id, Audit audit)
        {
            if (id != audit.Id)
            {
                return BadRequest();
            }

            _context.Entry(audit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditExists(id))
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

        // POST: api/Audit
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Audit>> PostAudit(Audit audit)
        {
            _context.Audits.Add(audit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAudit", new { id = audit.Id }, audit);
        }

        // DELETE: api/Audit/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Audit>> DeleteAudit(Guid id)
        {
            var audit = await _context.Audits.FindAsync(id);
            if (audit == null)
            {
                return NotFound();
            }

            _context.Audits.Remove(audit);
            await _context.SaveChangesAsync();

            return audit;
        }

        private bool AuditExists(Guid id)
        {
            return _context.Audits.Any(e => e.Id == id);
        }
    }
}