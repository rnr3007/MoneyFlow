using Microsoft.EntityFrameworkCore;

namespace MoneyFlow.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options) : base(options)
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