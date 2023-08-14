using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyFlow.Data;
using MoneyFlow.Models;
using System;
using od = MoneyFlow.Constants.OrderConstants;
using MoneyFlow.Constants;
using System.Data;
using MoneyFlow.Models.DetailComponents;

namespace MoneyFlow.Services
{
    public class IncomeService
    {
        private readonly DatabaseContext _dbContext;

        public IncomeService(DatabaseContext dbContext)
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

        public async Task<TableView<Income>> GetIncomes(string userId, int page, int limit, string keyword, string order)
        {
            IQueryable<Income> query = _dbContext.TIncome.AsQueryable()
                .Where(x => x.UserId == userId && (
                    x.IncomeMoney.ToString().Contains(keyword)
                ));
                
            int totalData = query.Count();

            Pagination paginationView = new Pagination(
                page,
                limit,
                totalData
            );
            paginationView.Order = order;

            string[] orders = order.Split("|");
            query = orders[0] switch
            {
                od.ORDER_BY_MONEY => od.ASC == orders[1] ? query.OrderBy(x => x.IncomeMoney) : query.OrderByDescending(x => x.IncomeMoney),
                od.ORDER_BY_DATE => od.ASC == orders[1] ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt),
                _ => query.OrderByDescending(x => x.CreatedAt),
            };

            List<Income> incomes = await query
                .Skip(paginationView.LimitData * (paginationView.ChoosenPage - 1))
                .Take(paginationView.LimitData)
                .ToListAsync();

            TableView<Income> tableView = new TableView<Income>(
                incomes,
                paginationView
            );

            tableView.SearchBarView = new SearchBar(
                keyword
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