using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisitionApi.Models;

namespace gpcalanderbackenddotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class organisationController : ControllerBase
    {
        private readonly OrganisationContext _context;

        public organisationController(OrganisationContext context)
        {
            _context = context;
        }

        // GET: api/organisation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organisation>>> GetEvent()
        {
            return await _context.Organisation.ToListAsync();
        }

        // GET: api/organisation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organisation>> Getorganisation(long id)
        {
            var organisation = await _context.Organisation.FindAsync(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return organisation;
        }

        // PUT: api/organisation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganisation(long id, Organisation organisation)
        {
            if (id != organisation.Id)
            {
                return BadRequest();
            }

            _context.Entry(organisation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!organisationExists(id))
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

        // POST: api/organisation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Organisation>> Postorganisation(Organisation organisation)
        {
            _context.Organisation.Add(organisation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getorganisation", new { id = organisation.Id }, organisation);
        }

        // DELETE: api/organisation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteorganisation(long id)
        {
            var organisation = await _context.Organisation.FindAsync(id);
            if (organisation == null)
            {
                return NotFound();
            }

            _context.Organisation.Remove(organisation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool organisationExists(long id)
        {
            return _context.Organisation.Any(e => e.Id == id);
        }
        [HttpGet]
        [Route("~/findorgbyuserid")] 
        public async  Task<ActionResult<List<Organisation>>> findClientsByUserId([FromQuery(Name = "created_user_id")] long user_id){
            //string username  = Request.Form["username"];
            List<Organisation> organisList= await _context.Organisation.Where(client=>client.Created_user_id == user_id).ToListAsync();
            
            return organisList;
        }
    }
}
