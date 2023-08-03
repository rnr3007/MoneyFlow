using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MoneyFlow.Context;

namespace MoneyFlow.Services
{
    public class SavingsService
    {
        private readonly UserContext _dbContext;

        public SavingsService(UserContext dbContext)
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