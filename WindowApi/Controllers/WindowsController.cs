using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WindowApi.Models;

namespace WindowApi.Controllers
{
    public class WindowsController : Controller
    {
        private readonly WindowContext _context;

        public WindowsController(WindowContext context)
        {
            _context = context;
        }

        // GET: Windows
        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoItems.ToListAsync());
        }

        // GET: Windows/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var window = await _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (window == null)
            {
                return NotFound();
            }

            return View(window);
        }

        // GET: Windows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Windows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsComplete")] Window window)
        {
            if (ModelState.IsValid)
            {
                _context.Add(window);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(window);
        }

        // GET: Windows/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var window = await _context.TodoItems.FindAsync(id);
            if (window == null)
            {
                return NotFound();
            }
            return View(window);
        }

        // POST: Windows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,IsComplete")] Window window)
        {
            if (id != window.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(window);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WindowExists(window.Id))
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
            return View(window);
        }

        // GET: Windows/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var window = await _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (window == null)
            {
                return NotFound();
            }

            return View(window);
        }

        // POST: Windows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var window = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(window);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WindowExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
