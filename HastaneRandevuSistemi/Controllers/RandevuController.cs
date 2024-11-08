using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HastaneRandevuSistemi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly HastaneDbContext _context;

        public RandevuController(HastaneDbContext context)
        {
            _context = context;
        }

        // Uzmanlık alanlarını doldurmak için
        public IActionResult Create()
        {
            ViewBag.UzmanlikAlanlari = _context.Doktorlar
                .Select(d => d.UzmanlikAlani)
                .Distinct()
                .ToList();

            return View();
        }

        [HttpGet]
        public JsonResult DoktorlariGetir(string uzmanlikAlani)
        {
            var doktorlar = _context.Doktorlar
                .Where(d => d.UzmanlikAlani == uzmanlikAlani)
                .Select(d => new { d.DoktorId, d.Ad, d.Soyad })
                .ToList();

            return Json(doktorlar);
        }

        [HttpPost]
        public IActionResult RandevuOlustur(string hastaAd, string hastaSoyad, string hastaTC, int doktorId, string randevuTarihi, string randevuSaati, string randevuSebebi)
        {
            var hastaId = 1; // Giriş yapan kullanıcının ID'si (Statik örnek)

            var randevu = new Randevu
            {
                HastaAd = hastaAd,
                HastaSoyad = hastaSoyad,
                HastaTC = hastaTC,
                HastaId = hastaId,
                DoktorId = doktorId,
                RandevuTarihi = DateTime.Parse(randevuTarihi),
                RandevuSaati = randevuSaati,
                RandevuSebebi = randevuSebebi,
                IptalEdildi = false
            };

            _context.Randevular.Add(randevu);
            _context.SaveChanges();

            return RedirectToAction("RandevuSuccess");
        }

        // Randevu başarılı mesajı
        public IActionResult RandevuSuccess()
        {
            return View();
        }
    }
}
