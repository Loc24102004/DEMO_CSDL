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
    public class NhatKiesController : Session
    {
        private readonly ApplicationDbContext _context;

        public NhatKiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NhatKies
        // GET: NhatKies
        public async Task<IActionResult> Index()
        {
            // Lấy giá trị UserId (có thể là null)
            var userid = UserId;

            // Kiểm tra xem userid có giá trị không
            if (!userid.HasValue)
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            

            // Truy vấn nhatky dựa trên Idtk
            var nhatkies = await _context.NhatKies
                .Include(n => n.IdloainkNavigation)
                .Include(n => n.IdtkNavigation)
                .Include(n => n.IdtrangthaiNavigation)
                .Where(n => n.Idtk == UserId) // Sử dụng userIdInt ở đây
                .ToListAsync(); // Thực hiện truy vấn

            // Trả về view với kết quả nhatky
            return View(nhatkies);
        }




        // GET: NhatKies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhatKy = await _context.NhatKies
                .Include(n => n.IdloainkNavigation)
                .Include(n => n.IdtkNavigation)
                .Include(n => n.IdtrangthaiNavigation)
                .FirstOrDefaultAsync(m => m.Idnk == id);
            if (nhatKy == null)
            {
                return NotFound();
            }

            return View(nhatKy);
        }

        // GET: NhatKies/Create
        public IActionResult Create()
        {
            ViewData["Idloaink"] = new SelectList(_context.LoaiNks, "Idloaink", "Tenloai");
            ViewData["Idtrangthai"] = new SelectList(_context.TrangThais, "Idtrangthai", "Tentrangthai");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhatKy nhatKy)
        {
            if (!ModelState.IsValid)
            {
                var UserId = HttpContext.Session.GetInt32("UserId");
                if (!UserId.HasValue)
                {
                    ModelState.AddModelError("", "UserId không hợp lệ.");
                    return View(nhatKy);
                }

                nhatKy.Idtk = UserId.Value; // Gán UserId vào nhatKy

                _context.Add(nhatKy); // Thêm đối tượng nhatKy vào DbContext
                await _context.SaveChangesAsync(); // Lưu vào cơ sở dữ liệu

                return RedirectToAction(nameof(Index)); // Chuyển hướng đến trang Index
            }

            // Nếu ModelState không hợp lệ, hiển thị lại form tạo
            ViewData["Idloaink"] = new SelectList(_context.LoaiNks, "Idloaink", "Tenloai");
            ViewData["Idtrangthai"] = new SelectList(_context.TrangThais, "Idtrangthai", "Tentrangthai");
            return View(nhatKy);
        }


        // GET: NhatKies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhatKy = await _context.NhatKies.FindAsync(id);
            if (nhatKy == null)
            {
                return NotFound();
            }
            ViewData["Idloaink"] = new SelectList(_context.LoaiNks, "Idloaink", "Idloaink", nhatKy.Idloaink);
            ViewData["Idtk"] = new SelectList(_context.TaiKhoans, "Idtk", "Idtk", nhatKy.Idtk);
            ViewData["Idtrangthai"] = new SelectList(_context.TrangThais, "Idtrangthai", "Idtrangthai", nhatKy.Idtrangthai);
            return View(nhatKy);
        }

        // POST: NhatKies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idnk,Idloaink,Idtrangthai,Tieude,Noidung,Mota,Hinhanh,Video,Ngaytao,Ngaychinhsua,Nguoichinhsua,Nguoitao,Idtk")] NhatKy nhatKy)
        {
            if (id != nhatKy.Idnk)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhatKy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhatKyExists(nhatKy.Idnk))
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
            ViewData["Idloaink"] = new SelectList(_context.LoaiNks, "Idloaink", "Idloaink", nhatKy.Idloaink);
            ViewData["Idtk"] = new SelectList(_context.TaiKhoans, "Idtk", "Idtk", nhatKy.Idtk);
            ViewData["Idtrangthai"] = new SelectList(_context.TrangThais, "Idtrangthai", "Idtrangthai", nhatKy.Idtrangthai);
            return View(nhatKy);
        }

        // GET: NhatKies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhatKy = await _context.NhatKies
                .Include(n => n.IdloainkNavigation)
                .Include(n => n.IdtkNavigation)
                .Include(n => n.IdtrangthaiNavigation)
                .FirstOrDefaultAsync(m => m.Idnk == id);
            if (nhatKy == null)
            {
                return NotFound();
            }

            return View(nhatKy);
        }

        // POST: NhatKies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhatKy = await _context.NhatKies.FindAsync(id);
            if (nhatKy != null)
            {
                _context.NhatKies.Remove(nhatKy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhatKyExists(int id)
        {
            return _context.NhatKies.Any(e => e.Idnk == id);
        }
    }
}
