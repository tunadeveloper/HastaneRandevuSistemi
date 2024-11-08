using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Controllers
{
    public class HastalarController : Controller
    {
        private readonly HastaneDbContext _context;

        public HastalarController(HastaneDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hastalar = _context.Hastalar.ToList();
            return View(hastalar);
        }
    }
}
