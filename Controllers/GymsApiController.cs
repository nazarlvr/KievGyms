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
    public class GymsApiController : ControllerBase
    {
        private readonly GymDBContext _context;

        public GymsApiController(GymDBContext context)
        {
            _context = context;
        }

        // GET: api/GymsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gym>>> GetGyms()
        {
            return await _context.Gyms.ToListAsync();
        }

        // GET: api/GymsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gym>> GetGym(int id)
        {
            var gym = await _context.Gyms.FindAsync(id);

            if (gym == null)
            {
                return NotFound();
            }

            return gym;
        }

        // PUT: api/GymsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGym(int id, Gym gym)
        {
            if (id != gym.GymId)
            {
                return BadRequest();
            }

            _context.Entry(gym).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GymExists(id))
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

        // POST: api/GymsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gym>> PostGym(Gym gym)
        {
            _context.Gyms.Add(gym);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGym", new { id = gym.GymId }, gym);
        }

        // DELETE: api/GymsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGym(int id)
        {
            var gym = await _context.Gyms.FindAsync(id);
            if (gym == null)
            {
                return NotFound();
            }

            _context.Gyms.Remove(gym);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GymExists(int id)
        {
            return _context.Gyms.Any(e => e.GymId == id);
        }
    }
}
