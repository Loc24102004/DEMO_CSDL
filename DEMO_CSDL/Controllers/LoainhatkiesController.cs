using DEMO_CSDL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;

namespace DEMO_CSDL.Controllers
{
    public class LoainhatkiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoainhatkiesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LoaiNks;
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tenloai,Mota,Ngaytao,Ngaychinh,Nguoichinhsua,Nguoitao")] LoaiNk loaink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaink = await _context.LoaiNks
                .FirstOrDefaultAsync(l => l.Idloaink == id);

            if (loaink == null)
            {
                return NotFound();
            }

            // Cập nhật các bản ghi tham chiếu để trỏ đến một giá trị hợp lệ khác
            var nhatKies = await _context.NhatKies
                .Where(n => n.Idloaink == id)
                .ToListAsync();

            foreach (var nhatKy in nhatKies)
            {
                nhatKy.Idloaink = null;  // Hoặc cập nhật giá trị khác
                _context.NhatKies.Update(nhatKy);
            }

            // Xóa bản ghi chính
            _context.LoaiNks.Remove(loaink);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiNk = await _context.LoaiNks.FindAsync(id); // Truyền id vào FindAsync
            if (loaiNk == null)
            {
                return NotFound();
            }

            return View(loaiNk);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(LoaiNk loaink)
        {
            _context.Entry(loaink).State = EntityState.Modified;
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }

    }

    }
