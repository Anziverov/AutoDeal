using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace AutoDeal.Models
{
    public class AutoDealContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public AutoDealContext( DbContextOptions options) : base(options)
        {
        }

    }
}
