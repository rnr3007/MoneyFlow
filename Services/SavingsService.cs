using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MoneyFlow.Data;
using MoneyFlow.Data.Views;
using System;

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

            UserAndMoney userAndMoney = await _dbContext.VUserAndMoney
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            long totalExpenses = userAndMoney.TotalExpense;
            long totalIncome = userAndMoney.TotalIncome;

            return totalIncome - totalExpenses;
        }
    }
}