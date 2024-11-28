using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DEMO_CSDL.Models;

namespace DEMO_CSDL.Controllers
{
    public class TrangThaisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrangThaisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrangThais
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrangThais.ToListAsync());
        }

        // GET: TrangThais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TrangThais
                .FirstOrDefaultAsync(m => m.Idtrangthai == id);
            if (trangThai == null)
            {
                return NotFound();
            }

            return View(trangThai);
        }

        // GET: TrangThais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrangThais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tentrangthai,Mota")] TrangThai trangThai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trangThai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trangThai);
        }

        // GET: TrangThais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TrangThais.FindAsync(id);
            if (trangThai == null)
            {
                return NotFound();
            }
            return View(trangThai);
        }

        // POST: TrangThais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idtrangthai,Tentrangthai,Mota")] TrangThai trangThai)
        {
            if (id != trangThai.Idtrangthai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trangThai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrangThaiExists(trangThai.Idtrangthai))
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
            return View(trangThai);
        }

        // GET: TrangThais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TrangThais
                .FirstOrDefaultAsync(m => m.Idtrangthai == id);
            if (trangThai == null)
            {
                return NotFound();
            }

            return View(trangThai);
        }

        // POST: TrangThais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trangThai = await _context.TrangThais.FindAsync(id);
            if (trangThai != null)
            {
                _context.TrangThais.Remove(trangThai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrangThaiExists(int id)
        {
            return _context.TrangThais.Any(e => e.Idtrangthai == id);
        }
    }
}
