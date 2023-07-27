using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Context;
using MoneyFlow.Models;
using MoneyFlow.Models.ViewModels;
using System;
using System.Data;
using MoneyFlow.Constants;
using Microsoft.AspNetCore.Http;
using fu = MoneyFlow.Utils.FileUtilites;

namespace MoneyFlow.Services
{
    public class ExpenseService
    {

        private readonly UserContext _dbContext;

        private readonly static string baseUrl = Startup.StaticConfiguration.GetSection("BASE_URL").Value;

        public ExpenseService(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Expense> GetExpense(string userId, string expenseId)
        {
            return await _dbContext.TExpense.Where(x => x.UserId == userId && x.Id == expenseId).FirstOrDefaultAsync() 
                ?? throw new DataException(ErrorMessage.EXPENSE_NOT_FOUND);
        }

        public async Task<TableViewModel<Expense>> GetExpenseList(string userId, int page, int limit, string keyword)
        {
            int totalData = _dbContext.TExpense
                .Where(x => x.UserId == userId && x.Name.Contains(keyword))
                .Count();

            PaginationViewModel paginationView = new PaginationViewModel(
                page, 
                limit, 
                totalData, 
                keyword, 
                $"{baseUrl}/expense");

            List<Expense> userExpenses = await _dbContext.TExpense
                .Where(x => x.UserId == userId && x.Name.Contains(keyword))
                .Skip(paginationView.LimitData * (paginationView.ChoosenPage - 1))
                .Take(paginationView.LimitData)
                .ToListAsync();

            return new TableViewModel<Expense>(
                userExpenses,
                paginationView
            );
        }

        public async Task UpdateExpense(string userId, string expenseId, Expense newExpense, IFormFile formFile)
        {
            Expense oldExpense = await GetExpense(userId, expenseId);
            oldExpense.Name = newExpense.Name;
            oldExpense.CostType = newExpense.CostType;
            oldExpense.Cost = newExpense.Cost;
            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteExpense(string userId, string expenseId)
        {
            _dbContext.TExpense.Remove(await GetExpense(userId, expenseId));
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateExpense(string userId, Expense expense, IFormFile formFile)
        {
            if (formFile != null)
            {
                expense.ReceiptFile = await fu.SaveFile("expense", formFile, userId);
            }
            _dbContext.TExpense.Add(expense);
            await _dbContext.SaveChangesAsync();
        }
    }
}