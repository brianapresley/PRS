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
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsAPIController : ControllerBase
    {
        private readonly SqlPRSContext _context;

        public VendorsAPIController(SqlPRSContext context)
        {
            _context = context;
        }

        // GET: api/VendorsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendors>>> GetVendors()
        {
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/VendorsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendors>> GetVendors(int id)
        {
            var vendors = await _context.Vendors.FindAsync(id);

            if (vendors == null)
            {
                return NotFound();
            }

            return vendors;
        }

        // PUT: api/VendorsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendors(int id, Vendors vendors)
        {
            if (id != vendors.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorsExists(id))
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

        // POST: api/VendorsAPI
        [HttpPost]
        public async Task<ActionResult<Vendors>> PostVendors(Vendors vendors)
        {
            _context.Vendors.Add(vendors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendors", new { id = vendors.Id }, vendors);
        }

        // DELETE: api/VendorsAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendors>> DeleteVendors(int id)
        {
            var vendors = await _context.Vendors.FindAsync(id);
            if (vendors == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendors);
            await _context.SaveChangesAsync();

            return vendors;
        }

        private bool VendorsExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
