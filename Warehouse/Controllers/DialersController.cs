using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class DialersController : Controller
    {
        private readonly WarehouseContext _context;

        public DialersController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: Dialers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dialers.ToListAsync());
        }

        // GET: Dialers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dialer = await _context.Dialers
                .FirstOrDefaultAsync(m => m.DialerId == id);
            if (dialer == null)
            {
                return NotFound();
            }

            return View(dialer);
        }

        // GET: Dialers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dialers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DialerId,DialerName,DialerAddress,TelNumber")] Dialer dialer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dialer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dialer);
        }

        // GET: Dialers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dialer = await _context.Dialers.FindAsync(id);
            if (dialer == null)
            {
                return NotFound();
            }
            return View(dialer);
        }

        // POST: Dialers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DialerId,DialerName,DialerAddress,TelNumber")] Dialer dialer)
        {
            if (id != dialer.DialerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dialer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DialerExists(dialer.DialerId))
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
            return View(dialer);
        }

        // GET: Dialers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dialer = await _context.Dialers
                .FirstOrDefaultAsync(m => m.DialerId == id);
            if (dialer == null)
            {
                return NotFound();
            }

            return View(dialer);
        }

        // POST: Dialers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dialer = await _context.Dialers.FindAsync(id);
            _context.Dialers.Remove(dialer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DialerExists(int id)
        {
            return _context.Dialers.Any(e => e.DialerId == id);
        }
    }
}
