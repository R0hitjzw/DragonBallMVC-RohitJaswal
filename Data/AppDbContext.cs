using DragonBallMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DragonBallMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Favorite> Favorites { get; set; }
    }
}