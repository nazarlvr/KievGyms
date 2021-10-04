using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KievGyms;

namespace KievGyms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymMembershipsApiController : ControllerBase
    {
        private readonly GymDBContext _context;

        public GymMembershipsApiController(GymDBContext context)
        {
            _context = context;
        }

        // GET: api/GymMembershipsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymMembership>>> GetGymMemberships()
        {
            return await _context.GymMemberships.ToListAsync();
        }

        // GET: api/GymMembershipsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GymMembership>> GetGymMembership(int id)
        {
            var gymMembership = await _context.GymMemberships.FindAsync(id);

            if (gymMembership == null)
            {
                return NotFound();
            }

            return gymMembership;
        }

        // PUT: api/GymMembershipsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGymMembership(int id, GymMembership gymMembership)
        {
            if (id != gymMembership.GymMembershipId)
            {
                return BadRequest();
            }

            _context.Entry(gymMembership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GymMembershipExists(id))
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

        // POST: api/GymMembershipsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GymMembership>> PostGymMembership(GymMembership gymMembership)
        {
            _context.GymMemberships.Add(gymMembership);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGymMembership", new { id = gymMembership.GymMembershipId }, gymMembership);
        }

        // DELETE: api/GymMembershipsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymMembership(int id)
        {
            var gymMembership = await _context.GymMemberships.FindAsync(id);
            if (gymMembership == null)
            {
                return NotFound();
            }

            _context.GymMemberships.Remove(gymMembership);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GymMembershipExists(int id)
        {
            return _context.GymMemberships.Any(e => e.GymMembershipId == id);
        }
    }
}
