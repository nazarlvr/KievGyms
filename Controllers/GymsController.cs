using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KievGyms;

namespace KievGyms.Controllers
{
    public class GymsController : Controller
    {
        private readonly GymDBContext _context;

        public GymsController(GymDBContext context)
        {
            _context = context;
        }

        // GET: Gyms
        public async Task<IActionResult> Index()
        {
            var gymDBContext = _context.Gyms.Include(g => g.District);

            return View(await gymDBContext.ToListAsync());
        }

        // GET: Gyms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gym = await _context.Gyms
                .Include(g => g.District)
                .FirstOrDefaultAsync(m => m.GymId == id);
            if (gym == null)
            {
                return NotFound();
            }

            //return View(gym);
            return RedirectToAction("Index", "Trainers", new { id = gym.GymId, name = gym.GymName});
        }

        // GET: Gyms/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictName");
            return View();
        }

        // POST: Gyms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymId,GymName,DistrictId,GymInfo,GeoLat,GeoLong")] Gym gym)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gym);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictName", gym.DistrictId);
            return View(gym);
        }

        // GET: Gyms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gym = await _context.Gyms.FindAsync(id);
            if (gym == null)
            {
                return NotFound();
            }
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictName", gym.DistrictId);
            return View(gym);
        }

        // POST: Gyms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymId,GymName,DistrictId,GymInfo,GeoLat,GeoLong")] Gym gym)
        {
            if (id != gym.GymId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gym);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymExists(gym.GymId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictName", gym.DistrictId);
            return View(gym);
        }

        // GET: Gyms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gym = await _context.Gyms
                .Include(g => g.District)
                .FirstOrDefaultAsync(m => m.GymId == id);
            if (gym == null)
            {
                return NotFound();
            }

            return View(gym);
        }

        // POST: Gyms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gym = await _context.Gyms.FindAsync(id);
            _context.Gyms.Remove(gym);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymExists(int id)
        {
            return _context.Gyms.Any(e => e.GymId == id);
        }
    }
}
