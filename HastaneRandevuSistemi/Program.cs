using HastaneRandevuSistemi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Di�er servisleri ekleyin
builder.Services.AddDbContext<HastaneDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HastaneDb")));

// Cookie tabanl� kimlik do�rulama servisini ekleyin
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Doktor/Login"; // Giri� sayfas� URL'si
        options.LogoutPath = "/Doktor/Logout"; // ��k�� sayfas� URL'si
        options.AccessDeniedPath = "/Doktor/AccessDenied"; // Eri�im engellendi sayfas� URL'si
    });

// Di�er servisleri ekleyin
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
        c.RoutePrefix = string.Empty; // Ana dizinde Swagger aray�z� i�in
    });
}

app.UseHttpsRedirection();

// Kimlik do�rulama ve yetkilendirme i�lemleri
app.UseAuthentication(); // Kimlik do�rulama
app.UseAuthorization();  // Yetkilendirme

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
