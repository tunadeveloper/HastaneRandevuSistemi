using Microsoft.AspNetCore.Mvc;
using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HastaneRandevuSistemi.Controllers
{
    public class DoktorController : Controller
    {
        private readonly HastaneDbContext _context;

        public DoktorController(HastaneDbContext context)
        {
            _context = context;
        }

        // Giriş sayfasını GET metodu ile yönlendirme
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login işlemi POST metodu ile yapılacak
        [HttpPost]
        public IActionResult Login(string tckimlikNo, string sifre)
        {
            var doktor = _context.Doktorlar
                .FirstOrDefault(d => d.TCKimlikNo == tckimlikNo && d.Sifre == sifre);

            if (doktor != null)
            {
                // Giriş başarılı, cookie oluşturma
                Response.Cookies.Append("DoktorId", doktor.DoktorId.ToString());
                return RedirectToAction("RandevuListesi");
            }

            // Giriş başarısız
            ViewData["ErrorMessage"] = "Hatalı kimlik bilgisi.";
            return View();
        }

        // Giriş yaptıktan sonra yönlendirilecek sayfa
        public IActionResult RandevuListesi()
        {
            // DoktorId'yi cookie'den al
            var doktorId = Request.Cookies["DoktorId"];
            if (doktorId == null)
            {
                return RedirectToAction("Login");
            }

            // Doktora ait randevular ve hastalar
            var randevular = _context.Randevular
                .Where(r => r.DoktorId == int.Parse(doktorId) && !r.IptalEdildi)
                .Include(r => r.Hasta) // Randevudaki hastayı dahil et
                .ToList();

            // View'a randevular ve hastalar
            return View(randevular);
        }
    }
}
