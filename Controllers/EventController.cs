using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventApi.Models;
using MedicalRecordApi.Models;
using System.Collections.Immutable;

namespace gpcalanderbackenddotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventContext _context;
        private readonly MedicalRecordContext _medicalRecordContext;

        ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("gpcalanderbackenddotnet-EventController");

        public EventController(EventContext context,MedicalRecordContext medicalRecordContext)
        {
            _context = context;
            _medicalRecordContext = medicalRecordContext;
            
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            return await _context.Event.ToListAsync();
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(long id)
        {
            var @event = await _context.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Event/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(long id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Event/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(long id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(long id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
        [HttpGet]
        [Route("~/findeventsbyuserid")] 
        public async  Task<ActionResult<List<Event>>> findEventsByUserId([FromQuery(Name = "user_id")] long user_id,[FromQuery(Name = "current_date")] DateOnly current_date){
           //string username  = Request.Form["username"];
           //https://code-maze.com/aspnetcore-pass-parameters-to-http-get-action/
            DateOnly last_month = current_date.AddMonths(-1).AddDays(-10);
            DateOnly next_month = current_date.AddMonths(1).AddDays(-20);;
            //https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager
            List<Event> eventList= await _context.Event.Where(cevent=>cevent.Created_user_id == user_id && cevent.Eventdate >=last_month && cevent.Eventdate <= next_month)
            .Include(cevent=>cevent.Client)
            .Include(cevent=>cevent.Organisation).ToListAsync();
            return eventList;
        }
        [HttpPost]
        [Route("~/createevent")] 
        public async  Task<IActionResult> createEvent(Event @event){
            @event.Create_time = DateTime.Now;
            @event.Modified_time = DateTime.Now;
            @event.Modified_user_id = @event.Created_user_id;
            logger.LogInformation("@Event_id before: " + @event.Id);
            await PostEvent(@event);
            logger.LogInformation("@Event_id after:" + @event.Id);
            List<MedicalRecord> medicalRecordList = await _medicalRecordContext.MedicalRecord.
                                    Where(medicalRecord => medicalRecord.Eventid == @event.Id).ToListAsync();
            if(medicalRecordList.Count==0){
                MedicalRecord medicalRecord =  new MedicalRecord();
                medicalRecord.Eventid = @event.Id;
                _medicalRecordContext.Add(medicalRecord);
                await _medicalRecordContext.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost]
        [Route("~/updateevent")] 
        public async  Task<IActionResult> updateeventupdateclient(Event @event){
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Event updateevent = await _context.Event.Where(org => org.Id == @event.Id).FirstOrDefaultAsync();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            updateevent.Eventcmt = @event.Eventcmt;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            updateevent.Client_id = @event.Client_id;
            updateevent.Org_id = @event.Org_id;
            updateevent.Assigned_to = @event.Assigned_to;
            updateevent.Eventdate = @event.Eventdate;
            updateevent.EventEndDate = @event.EventEndDate;
            updateevent.StartTimeStr = @event.StartTimeStr;
            updateevent.EndTimeStr = @event.EndTimeStr;
            updateevent.Modified_time = DateTime.Now;
            updateevent.Modified_user_id = @event.Modified_user_id;
            updateevent.StartTimeForSql = @event.StartTimeForSql;
            updateevent.EndTimeForSql = @event.EndTimeForSql;
            updateevent.ReportStatus = @event.ReportStatus;
            return await PutEvent(updateevent.Id,updateevent);
        }
    } 
}
