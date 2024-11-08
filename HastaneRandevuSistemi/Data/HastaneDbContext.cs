using HastaneRandevuSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Data
{
    public class HastaneDbContext : DbContext
    {
        public HastaneDbContext(DbContextOptions<HastaneDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hasta> Hastalar { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
    }
}
