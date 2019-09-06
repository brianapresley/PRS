using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSProjectV1.Models;

namespace PRSProjectV1.Controllers
{
    [Route("api/Request")]
    [ApiController]
    public class RequestsAPIController : ControllerBase
    {
        private readonly SqlPRSContext _context;

        public RequestsAPIController(SqlPRSContext context)
        {
            _context = context;
        }

        // GET: api/RequestsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Request.ToListAsync();
        }

        // GET: api/GetRequestsForReview
        [Route("/api/GetRequestsForReview")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsForReview()
        {
            return await _context // the database
                            .Request // the entity
                            .Where( r => r.Status == "Review") //filter for review
                            .ToListAsync(); //return a collection

            //Query syntax 
            //var items = from r in _context.Request
            //            where r.Status == "Review"
            //            select r;
            // return await items.ToListAsync();
        }

        // GET: api/RequestsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // GET: api/RequestsAPI/5
        [Route("/api/SetStatusReview/{id}")]
        [HttpGet]
        public async Task<ActionResult<Request>> SetStatusReview(int id)
        {
            var request = await _context.Request.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }
            request.Status = "Review";
            _context.SaveChanges();

            return Ok();
        }
        
        // GET: api/RequestsAPI/5
        [Route("/api/SetStatusApproved/")]
        [HttpGet]
        public async Task<ActionResult<Request>> SetStatusApproved(int id)
        {
            var request = await _context.Request.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }
            request.Status = "Approved";
            _context.SaveChanges();

            return Ok();
        }
        
        // GET: api/RequestsAPI/5
        [Route("/api/SetStatusRejected/")]
        [HttpGet]
        public async Task<ActionResult<Request>> SetStatusRejected(int id)
        {
            var request = await _context.Request.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }
            request.Status = "Rejected";
            _context.SaveChanges();

            return Ok();
        }

        // PUT: api/RequestsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/RequestsAPI
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/RequestsAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }
    }
}
