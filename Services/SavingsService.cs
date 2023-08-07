using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MoneyFlow.Data;

namespace MoneyFlow.Services
{
    public class SavingsService
    {
        private readonly DatabaseContext _dbContext;

        public SavingsService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> GetSaving(string userId)
        {
            long totalExpenses = await _dbContext.TExpense
                .Where(x => x.UserId == userId)
                .SumAsync(x => x.Cost);

            long totalIncome = await _dbContext.TIncome
                .Where(x => x.UserId == userId)
                .SumAsync(x => x.IncomeMoney);

            return totalIncome - totalExpenses;
        }
    }
}