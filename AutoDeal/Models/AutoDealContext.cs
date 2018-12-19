using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace AutoDeal.Models
{
    public sealed class AutoDealContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TestDeal> TestDeals { get; set; }
        public AutoDealContext( DbContextOptions options) : base(options)
        {
        }

    }
}
