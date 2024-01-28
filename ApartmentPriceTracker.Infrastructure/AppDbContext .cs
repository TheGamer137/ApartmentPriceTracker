using Microsoft.EntityFrameworkCore;

namespace ApartmentPriceTracker.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Subscription> Subscriptions { get; set; }

    }
}
