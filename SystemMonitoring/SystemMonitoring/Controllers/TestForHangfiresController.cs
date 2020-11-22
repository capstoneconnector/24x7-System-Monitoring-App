using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemMonitoring.Backend.Data;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.Controllers
{
    public class TestForHangfiresController : Controller
    {
        private readonly DataContext _context;

        public TestForHangfiresController(DataContext context)
        {
            _context = context;
        }

        // GET: TestForHangfires
        public async Task<IActionResult> Index()
        {
            return View(await _context.TestForHangFire.ToListAsync());
        }

        // GET: TestForHangfires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testForHangfire = await _context.TestForHangFire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testForHangfire == null)
            {
                return NotFound();
            }

            return View(testForHangfire);
        }

        // GET: TestForHangfires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestForHangfires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Job,RunTime")] TestForHangfire testForHangfire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testForHangfire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testForHangfire);
        }

        // GET: TestForHangfires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testForHangfire = await _context.TestForHangFire.FindAsync(id);
            if (testForHangfire == null)
            {
                return NotFound();
            }
            return View(testForHangfire);
        }

        // POST: TestForHangfires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Job,RunTime")] TestForHangfire testForHangfire)
        {
            if (id != testForHangfire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testForHangfire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestForHangfireExists(testForHangfire.Id))
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
            return View(testForHangfire);
        }

        // GET: TestForHangfires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testForHangfire = await _context.TestForHangFire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testForHangfire == null)
            {
                return NotFound();
            }

            return View(testForHangfire);
        }

        // POST: TestForHangfires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testForHangfire = await _context.TestForHangFire.FindAsync(id);
            _context.TestForHangFire.Remove(testForHangfire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestForHangfireExists(int id)
        {
            return _context.TestForHangFire.Any(e => e.Id == id);
        }
    }
}
