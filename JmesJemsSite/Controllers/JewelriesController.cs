using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JmesJemsSite.Data;
using JmesJemsSite.Models;

namespace JmesJemsSite.Controllers
{
    public class JewelriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JewelriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jewelries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jewelry.ToListAsync());
        }

        // GET: Jewelries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelry
                .FirstOrDefaultAsync(m => m.JewelryId == id);
            if (jewelry == null)
            {
                return NotFound();
            }

            return View(jewelry);
        }

        // GET: Jewelries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jewelries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JewelryId,Title,Type,Color,Size,Price")] Jewelry jewelry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jewelry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jewelry);
        }

        // GET: Jewelries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelry.FindAsync(id);
            if (jewelry == null)
            {
                return NotFound();
            }
            return View(jewelry);
        }

        // POST: Jewelries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JewelryId,Title,Type,Color,Size,Price")] Jewelry jewelry)
        {
            if (id != jewelry.JewelryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jewelry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JewelryExists(jewelry.JewelryId))
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
            return View(jewelry);
        }

        // GET: Jewelries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelry
                .FirstOrDefaultAsync(m => m.JewelryId == id);
            if (jewelry == null)
            {
                return NotFound();
            }

            return View(jewelry);
        }

        // POST: Jewelries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jewelry = await _context.Jewelry.FindAsync(id);
            _context.Jewelry.Remove(jewelry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JewelryExists(int id)
        {
            return _context.Jewelry.Any(e => e.JewelryId == id);
        }
    }
}
