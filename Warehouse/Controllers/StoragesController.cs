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
    public class StoragesController : Controller
    {
        private readonly WarehouseContext _context;

        public StoragesController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: Storages
        public async Task<IActionResult> Index()
        {
            var warehouseContext = _context.Storages.Include(s => s.Dialer).Include(s => s.Product);
            return View(await warehouseContext.ToListAsync());
        }

        // GET: Storages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages
                .Include(s => s.Dialer)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.StorageId == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // GET: Storages/Create
        public IActionResult Create()
        {
            ViewData["DialerId"] = new SelectList(_context.Dialers, "DialerId", "DialerId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: Storages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StorageId,ReceiptDate,Volume,Cost,Employee,ProductId,DialerId")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DialerId"] = new SelectList(_context.Dialers, "DialerId", "DialerId", storage.DialerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", storage.ProductId);
            return View(storage);
        }

        // GET: Storages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages.FindAsync(id);
            if (storage == null)
            {
                return NotFound();
            }
            ViewData["DialerId"] = new SelectList(_context.Dialers, "DialerId", "DialerId", storage.DialerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", storage.ProductId);
            return View(storage);
        }

        // POST: Storages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StorageId,ReceiptDate,Volume,Cost,Employee,ProductId,DialerId")] Storage storage)
        {
            if (id != storage.StorageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageExists(storage.StorageId))
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
            ViewData["DialerId"] = new SelectList(_context.Dialers, "DialerId", "DialerId", storage.DialerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", storage.ProductId);
            return View(storage);
        }

        // GET: Storages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages
                .Include(s => s.Dialer)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.StorageId == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // POST: Storages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storage = await _context.Storages.FindAsync(id);
            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorageExists(int id)
        {
            return _context.Storages.Any(e => e.StorageId == id);
        }
    }
}
