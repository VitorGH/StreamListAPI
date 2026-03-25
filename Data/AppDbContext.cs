using Microsoft.EntityFrameworkCore;
using StreamListAPI.Models;

namespace StreamListAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<WatchlistItem> WatchlistItems => Set<WatchlistItem>();
    }
}
