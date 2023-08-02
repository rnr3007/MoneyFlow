using Microsoft.EntityFrameworkCore;
using MoneyFlow.Models;

namespace MoneyFlow.Context
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options) : base(options)
        {}

        public DbSet<User> TUser => Set<User>();
        public DbSet<Expense> TExpense => Set<Expense>();
        public DbSet<Income> TIncome => Set<Income>();
        public DbSet<Motivation> TMotivation => Set<Motivation>();

        protected override void OnModelCreating (ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Entity<User>(b =>
            {
                b.HasIndex(u => new {u.Id, u.Username, u.Email}).IsUnique();
            });
        }
    }
}