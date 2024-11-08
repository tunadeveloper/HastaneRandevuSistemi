using HastaneRandevuSistemi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Diðer servisleri ekleyin
builder.Services.AddDbContext<HastaneDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HastaneDb")));

// Cookie tabanlý kimlik doðrulama servisini ekleyin
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Doktor/Login"; // Giriþ sayfasý URL'si
        options.LogoutPath = "/Doktor/Logout"; // Çýkýþ sayfasý URL'si
        options.AccessDeniedPath = "/Doktor/AccessDenied"; // Eriþim engellendi sayfasý URL'si
    });

// Diðer servisleri ekleyin
builder.Services.AddControllersWithViews();

// Swagger servislerini ekle
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger middleware'ini ekle
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hastane API v1");
        c.RoutePrefix = string.Empty; // Ana dizinde Swagger arayüzü için
    });
}

app.UseHttpsRedirection();

// Kimlik doðrulama ve yetkilendirme iþlemleri
app.UseAuthentication(); // Kimlik doðrulama
app.UseAuthorization();  // Yetkilendirme

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
