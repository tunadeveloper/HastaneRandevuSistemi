using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Login(string TCKimlikNo, string Sifre)
        {
            // Doktoru veritabanında kontrol et
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.TCKimlikNo == TCKimlikNo && d.Sifre == Sifre);

            if (doktor != null)
            {
                // Kullanıcı bilgilerini ve rollerini içeren Claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, doktor.DoktorId.ToString()),
                    new Claim(ClaimTypes.Name, doktor.Ad),
                    new Claim(ClaimTypes.Role, "Doktor")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Kalıcı oturum
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                };

                // Cookie oluştur
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Doktor");
            }
            else
            {
                // Hatalı giriş
                ModelState.AddModelError("", "Geçersiz TC Kimlik No veya şifre.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Doktor");
        }
    }
}
