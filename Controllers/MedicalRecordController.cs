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
        ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("gpcalanderbackenddotnet-UserController");

        public MedicalRecordController(MedicalRecordContext context)
        {
            _context = context;
        }

        // GET: api/MedicalRecord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetEvent()
        {
            return await _context.MedicalRecord.ToListAsync();
        }

        // GET: api/MedicalRecord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(long id)
        {
            var medicalRecord = await _context.MedicalRecord.FindAsync(id);

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
            _context.MedicalRecord.Add(medicalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicalRecord", new { id = medicalRecord.Id }, medicalRecord);
        }

        // DELETE: api/MedicalRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(long id)
        {
            var medicalRecord = await _context.MedicalRecord.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            _context.MedicalRecord.Remove(medicalRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicalRecordExists(long id)
        {
            return _context.MedicalRecord.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("~/findmedicalrecordbyeventid")] 
        public async  Task<ActionResult<List<MedicalRecord>>> findEventsByUserId([FromQuery(Name = "event_id")] long event_id){
            List<MedicalRecord> medicalRecordList= 
                        await _context.MedicalRecord.Where(record=>record.Eventid == event_id).ToListAsync();
            if(medicalRecordList.Count == 0){
                MedicalRecord medicalRecord =  new MedicalRecord();
                medicalRecord.Eventid = event_id;
                _context.Add(medicalRecord);
                _context.SaveChanges();
                medicalRecordList.Add(medicalRecord);
            }
            return medicalRecordList;
        }

        [HttpPost]
        [Route("~/createmedicalrecord")] 
        public async  Task<ActionResult<List<MedicalRecord>>> createMedicalRecord(MedicalRecord medicalRecord){
            medicalRecord.Created_time = DateTime.Now;
            medicalRecord.Modified_time = DateTime.Now;
            medicalRecord.Modified_user_id = medicalRecord.Created_user_id;
            await PostMedicalRecord(medicalRecord);
            return Ok();
        }

        [HttpPost]
        [Route("~/updatemedicalrecord")] 
        public async  Task<ActionResult<List<MedicalRecord>>> updateMedicalRecord(MedicalRecord medicalRecord){

            MedicalRecord updateMedicalRecord = 
                        await _context.MedicalRecord.Where(record=>record.Id == medicalRecord.Id).FirstOrDefaultAsync();
            updateMedicalRecord.Summary = medicalRecord.Summary;
            updateMedicalRecord.Positions = medicalRecord.Positions;
            updateMedicalRecord.Modified_user_id = medicalRecord.Modified_user_id;
            updateMedicalRecord.Modified_time = DateTime.Now;
            await PutMedicalRecord(updateMedicalRecord.Id,updateMedicalRecord);
            return Ok();
        }
    }
}
