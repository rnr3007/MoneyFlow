using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyFlow.Context;
using MoneyFlow.Models;
using MoneyFlow.Models.ViewModels;
using System;
using MoneyFlow.Constants;
using System.Data;

namespace MoneyFlow.Services
{
    public class IncomeService
    {
        private readonly UserContext _dbContext;

        private readonly static string baseUrl = Startup.StaticConfiguration.GetSection("BASE_URL").Value;

        public IncomeService(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Income> GetIncome(string userId, string incomeId)
        {
            Income income = await _dbContext.TIncome
                .Where(x => x.UserId == userId && x.Id == incomeId)
                .FirstOrDefaultAsync() ?? throw new DataException(ErrorMessage.INCOME_NOT_FOUND);
            
            return income;
        }

        public async Task<TableViewModel<Income>> GetIncomes(string userId, int page, int limit, string keyword)
        {
            int totalData = _dbContext.TIncome
                .Where(x => x.UserId == userId && x.IncomeMoney.ToString().Contains(keyword))
                .Count();

            PaginationViewModel paginationView = new PaginationViewModel(
                page,
                limit,
                totalData,
                keyword,
                $"{baseUrl}/expenses"
            );

            List<Income> incomes = await _dbContext.TIncome
                .Where(x => x.UserId == userId && x.IncomeMoney.ToString().Contains(keyword))
                .ToListAsync();

            TableViewModel<Income> tableView = new TableViewModel<Income>(
                incomes,
                paginationView
            );

            return tableView;
        }

        public async Task CreateIncomes(string userId, Income income)
        {
            income.UserId = userId;
            _dbContext.TIncome.Add(income);
            await _dbContext.SaveChangesAsync();
        }
    
        public async Task UpdateIncomes(string userId, string incomeId, Income income)
        {
            Income incomeResult = await _dbContext.TIncome
                .Where(x => x.UserId == userId && x.Id == incomeId)
                .FirstOrDefaultAsync() ?? throw new DataException(ErrorMessage.INCOME_NOT_FOUND);

            incomeResult.IncomeMoney = income.IncomeMoney;
            incomeResult.IncomeType = income.IncomeType;
            incomeResult.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    
        public async Task DeleteIncomes(string userId, string incomeId) 
        {
            Income incomeResult = await _dbContext.TIncome
                .Where(x => x.UserId == userId && x.Id == incomeId)
                .FirstOrDefaultAsync() ?? throw new DataException(ErrorMessage.INCOME_NOT_FOUND);

            _dbContext.Remove(incomeResult);
            await _dbContext.SaveChangesAsync();
        }
    }
}