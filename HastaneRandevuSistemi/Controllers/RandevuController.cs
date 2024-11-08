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

        public IActionResult Create()
        {
            var uzmanlikAlanlari = _context.Doktorlar
                .Select(d => d.UzmanlikAlani)
                .Distinct()
                .ToList();

            return View(uzmanlikAlanlari);
        }

        [HttpGet]
        public JsonResult DoktorlariGetir(string uzmanlikAlani)
        {
            if (string.IsNullOrEmpty(uzmanlikAlani))
            {
                return Json(new { success = false, message = "Uzmanlık alanı seçilmedi." });
            }

            var doktorlar = _context.Doktorlar
                .Where(d => d.UzmanlikAlani == uzmanlikAlani)
                .Select(d => new { d.DoktorId, d.Ad, d.Soyad })
                .ToList();

            return Json(doktorlar);
        }


        [HttpPost]
        public IActionResult RandevuOlustur(string hastaAd, string hastaSoyad, string hastaTC, int doktorId, string randevuTarihi, string randevuSaati, string randevuSebebi)
        {
            var hastaId = 1;

            var randevu = new Randevu
            {
                HastaAd = hastaAd,
                HastaSoyad = hastaSoyad,
                HastaTC = hastaTC,
                HastaId = hastaId,
                DoktorId = doktorId,
                RandevuTarihi = DateTime.Parse(randevuTarihi),
                RandevuSaati = randevuSaati,
                RandevuSebebi = randevuSebebi
            };

            _context.Randevular.Add(randevu);
            _context.SaveChanges();

            return RedirectToAction("RandevuSuccess");
        }

        public IActionResult RandevuSuccess()
        {
            return View();
        }
    }
}
