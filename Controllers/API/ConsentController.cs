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
    public class ConsentController : ControllerBase
    {
        private readonly PowerServiceContext _context;

        public ConsentController(PowerServiceContext context)
        {
            _context = context;
        }
        // GET: api/Consent
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consent>>> GetConsents()
        {
            return await _context.Consents.ToListAsync();
        }

        // GET: api/Consent/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Consent>> GetConsent(Guid id)
        {
            var consents = await _context.Consents.FindAsync(id);

            if (consents == null)
            {
                return NotFound();
            }

            return consents;
        }

        // PUT: api/Consent/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsent(Guid id, Consent consent)
        {
            if (id != consent.Id)
            {
                return BadRequest();
            }

            _context.Entry(consent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsentExists(id))
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

        // POST: api/Consent
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Consent>> PostCase(Consent consent)
        {
            _context.Consents.Add(consent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsent", new { id = consent.Id }, consent);
        }

        // DELETE: api/Consent/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Consent>> DeleteConsent(Guid id)
        {
            var consent = await _context.Consents.FindAsync(id);
            if (consent == null)
            {
                return NotFound();
            }

            _context.Consents.Remove(consent);
            await _context.SaveChangesAsync();

            return consent;
        }

        private bool ConsentExists(Guid id)
        {
            return _context.Consents.Any(e => e.Id == id);
        }
    }
}