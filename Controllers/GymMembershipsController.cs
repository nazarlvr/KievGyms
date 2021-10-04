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
    public class GymMembershipsController : Controller
    {
        private readonly GymDBContext _context;

        public GymMembershipsController(GymDBContext context)
        {
            _context = context;
        }

        // GET: GymMemberships
        public async Task<IActionResult> Index()
        {
            var gymDBContext = _context.GymMemberships.Include(g => g.Client).Include(g => g.Gym);
            return View(await gymDBContext.ToListAsync());
        }

        // GET: GymMemberships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymMembership = await _context.GymMemberships
                .Include(g => g.Client)
                .Include(g => g.Gym)
                .FirstOrDefaultAsync(m => m.GymMembershipId == id);
            if (gymMembership == null)
            {
                return NotFound();
            }

            return View(gymMembership);
        }

        // GET: GymMemberships/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientFullName");
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName");
            return View();
        }

        // POST: GymMemberships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymMembershipId,GymId,ClientId,GymMembershipPrice,GymMembershipInfo,GymMembershipExpiryDate")] GymMembership gymMembership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymMembership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientFullName", gymMembership.ClientId);
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName", gymMembership.GymId);
            return View(gymMembership);
        }

        // GET: GymMemberships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymMembership = await _context.GymMemberships.FindAsync(id);
            if (gymMembership == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientFullName", gymMembership.ClientId);
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName", gymMembership.GymId);
            return View(gymMembership);
        }

        // POST: GymMemberships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymMembershipId,GymId,ClientId,GymMembershipPrice,GymMembershipInfo,GymMembershipExpiryDate")] GymMembership gymMembership)
        {
            if (id != gymMembership.GymMembershipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymMembership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymMembershipExists(gymMembership.GymMembershipId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientFullName", gymMembership.ClientId);
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName", gymMembership.GymId);
            return View(gymMembership);
        }

        // GET: GymMemberships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymMembership = await _context.GymMemberships
                .Include(g => g.Client)
                .Include(g => g.Gym)
                .FirstOrDefaultAsync(m => m.GymMembershipId == id);
            if (gymMembership == null)
            {
                return NotFound();
            }

            return View(gymMembership);
        }

        // POST: GymMemberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymMembership = await _context.GymMemberships.FindAsync(id);
            _context.GymMemberships.Remove(gymMembership);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymMembershipExists(int id)
        {
            return _context.GymMemberships.Any(e => e.GymMembershipId == id);
        }
    }
}
