using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientApi.Models;

namespace gpcalanderbackenddotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientContext _context;
        ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("gpcalanderbackenddotnet-UserController");


        public ClientController(ClientContext context)
        {
            _context = context;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetEvent()
        {
            return await _context.Client.ToListAsync();
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(long id)
        {
            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(long id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(long id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(long id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
        [HttpGet]
        [Route("~/findclientsbyuserid")] 
        public async  Task<ActionResult<List<Client>>> findClientsByUserId([FromQuery(Name = "user_id")] long user_id){
            //string username  = Request.Form["username"];
            List<Client> clientList= await _context.Client.Where(client=>client.Created_user_id == user_id).ToListAsync();
            
            return clientList;
        }

        [HttpPost]
        [Route("~/createclient")] 
        public async  Task<IActionResult> createoclient(Client client){
     
            List<Client> organisList= await _context.Client.Where(
                        clt=>clt.Created_user_id == client.Created_user_id).Where(
                        clt=>clt.Client_id_no == client.Client_id_no).ToListAsync();
            if(organisList.Count()>0){
                return Conflict();
            }else{
                client.Created_time = DateTime.Now;
                client.Modified_time = DateTime.Now;
                client.Modified_user_id = client.Created_user_id;
                await PostClient(client);
                return Ok();
            }
        }
        [HttpPost]
        [Route("~/updateclient")] 
        public async  Task<IActionResult> updateclient(Client client){
                Client updateclient= await _context.Client.Where(org=>org.Id == client.Id).FirstOrDefaultAsync();
                updateclient.Firstname = client.Firstname;
                updateclient.Middlename = client.Middlename;
                updateclient.Lastname = client.Lastname;
                updateclient.Birthday = client.Birthday;
                updateclient.Gender = client.Gender;
                updateclient.Modified_time = DateTime.Now;
                updateclient.Modified_user_id = client.Modified_user_id;
                return await PutClient(updateclient.Id,updateclient);
        }
    }
}
