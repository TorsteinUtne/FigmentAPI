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
    public class BillingController : ControllerBase
    {
        private readonly PowerServiceContext _context;

        public BillingController(PowerServiceContext context)
        {
            _context = context;
        }
        // GET: api/Billing
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Billing>>> GetBillings()
        {
            return await _context.Billings.ToListAsync();
        }

        // GET: api/Billing/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Billing>> GetBilling(Guid id)
        {
            var billings = await _context.Billings.FindAsync(id);

            if (billings == null)
            {
                return NotFound();
            }

            return billings;
        }

        // PUT: api/Billing/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBilling(Guid id, Billing billing)
        {
            if (id != billing.Id)
            {
                return BadRequest();
            }

            _context.Entry(billing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingExists(id))
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

        // POST: api/Billing
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Billing>> PostBilling(Billing billing)
        {
            _context.Billings.Add(billing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBilling", new { id = billing.Id }, billing);
        }

        // DELETE: api/Billing/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Billing>> DeleteBilling(Guid id)
        {
            var billing = await _context.Billings.FindAsync(id);
            if (billing == null)
            {
                return NotFound();
            }

            _context.Billings.Remove(billing);
            await _context.SaveChangesAsync();

            return billing;
        }

        private bool BillingExists(Guid id)
        {
            return _context.Billings.Any(e => e.Id == id);
        }
    }
}