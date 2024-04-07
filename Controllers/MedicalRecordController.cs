using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalRecordApi.Models;

namespace gpcalanderbackenddotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly MedicalRecordContext _context;

        public MedicalRecordController(MedicalRecordContext context)
        {
            _context = context;
        }

        // GET: api/MedicalRecord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetEvent()
        {
            return await _context.Event.ToListAsync();
        }

        // GET: api/MedicalRecord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(long id)
        {
            var medicalRecord = await _context.Event.FindAsync(id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return medicalRecord;
        }

        // PUT: api/MedicalRecord/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalRecord(long id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicalRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalRecordExists(id))
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

        // POST: api/MedicalRecord
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> PostMedicalRecord(MedicalRecord medicalRecord)
        {
            _context.Event.Add(medicalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicalRecord", new { id = medicalRecord.Id }, medicalRecord);
        }

        // DELETE: api/MedicalRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(long id)
        {
            var medicalRecord = await _context.Event.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            _context.Event.Remove(medicalRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicalRecordExists(long id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
