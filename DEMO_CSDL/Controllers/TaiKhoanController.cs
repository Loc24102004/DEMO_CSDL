using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DEMO_CSDL.Models;


namespace DEMO_CSDL.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaiKhoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var taikhoan = _context.TaiKhoans.ToList();
            return View(taikhoan);
        }

        // MD5 Hashing function
        public static string Md5Hash(string input, int count)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using (var md5Hash = MD5.Create())
            {
                var hash = input;
                for (var i = 0; i < count; i++)
                {
                    var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(hash));
                    var sBuilder = new StringBuilder();
                    foreach (var t in data)
                    {
                        sBuilder.Append(t.ToString("x2"));
                    }
                    hash = sBuilder.ToString();
                }
                return hash;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaiKhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                taikhoan.Mk = Md5Hash(taikhoan.Mk, 1);
                _context.Add(taikhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taikhoan);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null) return NotFound();

            var nhatKies = await _context.NhatKies.Where(n => n.Idtk == id).ToListAsync();
            foreach (var nhatKy in nhatKies)
            {
                nhatKy.Idtk = null;
                _context.NhatKies.Update(nhatKy);
            }

            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null) return NotFound();

            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.Idtk) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    taiKhoan.Mk = Md5Hash(taiKhoan.Mk, 1);
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.Idtk)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(taiKhoan);
        }

        private bool TaiKhoanExists(int id)
        {
            return _context.TaiKhoans.Any(e => e.Idtk == id);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new TaiKhoan());
        }

        [HttpPost]
        public IActionResult Register(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.TaiKhoans.FirstOrDefault(s => s.Email == taiKhoan.Email);

                if (existingUser == null)
                {
                    taiKhoan.Mk = Md5Hash(taiKhoan.Mk, 1);
                    taiKhoan.Ngaytao = DateTime.Now;
                    _context.Add(taiKhoan);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "This email is already registered.";
                }
            }
            return View(taiKhoan);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string tentk, string mk)
        {
            if (string.IsNullOrEmpty(tentk) || string.IsNullOrEmpty(mk))
            {
                ViewBag.ErrorMessage = "Username and password cannot be empty.";
                return View();
            }

            var hashedPassword = Md5Hash(mk, 1);
            var user = _context.TaiKhoans.FirstOrDefault(s => s.Tentk == tentk && s.Mk == hashedPassword);

            if (user != null)
            {
                // Lưu thông tin vào session
                HttpContext.Session.SetString("UserName", user.Tentk); // Lưu tên đăng nhập (hoặc tên người dùng)
                HttpContext.Session.SetInt32("UserId", user.Idtk);   // Lưu ID người dùng nếu cần

                // Chuyển hướng đến trang Index của NhatKies, với ID người dùng
                return RedirectToAction("Index", "NhatKies", new { id = user.Idtk });
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
            }

            return View();
        }


        public IActionResult Logout()
        {
            // Xóa session khi người dùng đăng xuất
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");

            // Chuyển hướng về trang Login
            return RedirectToAction("Login");
        }

    }
}
