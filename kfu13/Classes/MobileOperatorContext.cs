using Microsoft.EntityFrameworkCore;

namespace kfu13
{
    public class MobileOperatorContext : DbContext
    {
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Service> Services { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=kfu13;Username=postgres;Password=salavat");
            }
        }
    }

}




