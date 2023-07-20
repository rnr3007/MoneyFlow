using Microsoft.EntityFrameworkCore;
using MoneyFlow.Models;

namespace MoneyFlow.Context
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options) : base(options)
        {}

        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating (ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Entity<User>(b =>
            {
                b.HasIndex(u => new {u.Id, u.Username, u.Email}).IsUnique();
            });
            model.Entity<Product>(b =>
            {
                b.HasIndex(p => new {p.Id, p.Name}).IsUnique();
            });
        }
    }
}