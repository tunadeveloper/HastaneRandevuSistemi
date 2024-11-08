using HastaneRandevuSistemi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Diðer servisleri ekleyin
builder.Services.AddDbContext<HastaneDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HastaneDb")));

// MVC ve controller'lar için gerekli servis
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
app.UseAuthorization();

// MVC yönlendirmelerini ekle
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
