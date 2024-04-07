using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrgnasitionApi.Models;

namespace gpcalanderbackenddotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgansationController : ControllerBase
    {
        private readonly OrgansationContext _context;

        public OrgansationController(OrgansationContext context)
        {
            _context = context;
        }

        // GET: api/Organsation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organsation>>> GetEvent()
        {
            return await _context.Organsation.ToListAsync();
        }

        // GET: api/Organsation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organsation>> GetOrgansation(long id)
        {
            var organsation = await _context.Organsation.FindAsync(id);

            if (organsation == null)
            {
                return NotFound();
            }

            return organsation;
        }

        // PUT: api/Organsation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrgansation(long id, Organsation organsation)
        {
            if (id != organsation.Id)
            {
                return BadRequest();
            }

            _context.Entry(organsation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrgansationExists(id))
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

        // POST: api/Organsation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Organsation>> PostOrgansation(Organsation organsation)
        {
            _context.Organsation.Add(organsation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrgansation", new { id = organsation.Id }, organsation);
        }

        // DELETE: api/Organsation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrgansation(long id)
        {
            var organsation = await _context.Organsation.FindAsync(id);
            if (organsation == null)
            {
                return NotFound();
            }

            _context.Organsation.Remove(organsation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrgansationExists(long id)
        {
            return _context.Organsation.Any(e => e.Id == id);
        }
    }
}
