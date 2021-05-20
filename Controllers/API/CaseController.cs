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
    public class CaseController : ControllerBase
    {
        private readonly PowerServiceContext _context;

        public CaseController(PowerServiceContext context)
        {
            _context = context;
        }
        // GET: api/Cases
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Case>>> GetCases()
        {
            return await _context.Cases.ToListAsync();
        }

        // GET: api/Case/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Case>> GetCase(Guid id)
        {
            var caserecords = await _context.Cases.FindAsync(id);

            if (caserecords == null)
            {
                return NotFound();
            }

            return caserecords;
        }

        // PUT: api/Case/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCase(Guid id, Case caserecord)
        {
            if (id != caserecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(caserecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(id))
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

        // POST: api/Case
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Case>> PostCase(Case caserecord)
        {
            _context.Cases.Add(caserecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCase", new { id = caserecord.Id }, caserecord);
        }

        // DELETE: api/Case/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Case>> DeleteCase(Guid id)
        {
            var caserecord = await _context.Cases.FindAsync(id);
            if (caserecord == null)
            {
                return NotFound();
            }

            _context.Cases.Remove(caserecord);
            await _context.SaveChangesAsync();

            return caserecord;
        }

        private bool CaseExists(Guid id)
        {
            return _context.Cases.Any(e => e.Id == id);
        }
    }
}