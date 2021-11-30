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
    public class TrainersController : Controller
    {
        private readonly GymDBContext _context;

        public TrainersController(GymDBContext context)
        {
            _context = context;
        }

        // GET: Trainers
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) 
            {
                return RedirectToAction("Gyms", "Index") ;
            }
            ViewBag.GymId = id;
            ViewBag.GymName = name;
            var trainersbyGyms = _context.Trainers.Where(t => t.GymId == id).Include(t => t.Specialization).Include(g => g.Gym);
            return View(await trainersbyGyms.ToListAsync());
        }

        // GET: Trainers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .Include(t => t.Gym)
                .Include(t => t.Specialization)
                .FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // GET: Trainers/Create
        public IActionResult Create(int gymId)
        {
            ViewBag.GymId = gymId;
            ViewBag.GymName = _context.Gyms.Where(g=>g.GymId==gymId).FirstOrDefault().GymName;
            //ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName");
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "SpecializationName");
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int gymId, [Bind("TrainerId,TrainerFullName,TrainerDateOfBirth,TrainerSalary,SpecializationId")] Trainer trainer)
        {
            trainer.GymId = gymId;
            if (ModelState.IsValid)
            {
                _context.Add(trainer);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Gyms", new {id = gymId, name = _context.Gyms.Where(g => g.GymId == gymId).FirstOrDefault().GymName});
            }
            //ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymInfo", trainer.GymId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "SpecializationName", trainer.SpecializationId);
            //return View(trainer);
            return RedirectToAction("Index", "Gyms", new { id = gymId, name = _context.Gyms.Where(g => g.GymId == gymId).FirstOrDefault().GymName });
        }

        // GET: Trainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName", trainer.GymId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "SpecializationName", trainer.SpecializationId);
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainerId,TrainerFullName,TrainerDateOfBirth,GymId,TrainerSalary,SpecializationId")] Trainer trainer)
        {
            if (id != trainer.TrainerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.TrainerId))
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
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName", trainer.GymId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "SpecializationName", trainer.SpecializationId);
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .Include(t => t.Gym)
                .Include(t => t.Specialization)
                .FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerExists(int id)
        {
            return _context.Trainers.Any(e => e.TrainerId == id);
        }
    }
}
