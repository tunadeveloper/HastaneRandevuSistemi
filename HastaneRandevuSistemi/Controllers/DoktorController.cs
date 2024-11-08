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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string tckimlikNo, string sifre)
        {
            var doktor = _context.Doktorlar
                .FirstOrDefault(d => d.TCKimlikNo == tckimlikNo && d.Sifre == sifre);

            if (doktor != null)
            {
                Response.Cookies.Append("DoktorId", doktor.DoktorId.ToString());
                return RedirectToAction("RandevuListesi");
            }
            ViewData["ErrorMessage"] = "Hatalı kimlik bilgisi.";
            return View();
        }
        public IActionResult RandevuListesi()
        {
            var doktorId = Request.Cookies["DoktorId"];
            if (doktorId == null)
            {
                return RedirectToAction("Login");
            }

            var randevular = _context.Randevular
                .Where(r => r.DoktorId == int.Parse(doktorId))
                .Include(r => r.Hasta) 
                .ToList();

            return View(randevular);
        }
    }
}
