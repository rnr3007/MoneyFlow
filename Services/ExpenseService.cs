using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Data;
using MoneyFlow.Models;
using System;
using System.Data;
using MoneyFlow.Constants;
using Microsoft.AspNetCore.Http;
using fu = MoneyFlow.Utils.FileUtilites;
using du = MoneyFlow.Utils.DataExtractor;
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using MoneyFlow.Constants.Enum;

namespace MoneyFlow.Services
{
    public class ExpenseService
    {

        private readonly DatabaseContext _dbContext;

        private readonly static string baseUrl = Startup.StaticConfiguration.GetSection("BASE_URL").Value;

        public ExpenseService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Expense> GetExpense(string userId, string expenseId)
        {
            return await _dbContext.TExpense.Where(x => x.UserId == userId && x.Id == expenseId).FirstOrDefaultAsync() 
                ?? throw new DataException(ErrorMessage.EXPENSE_NOT_FOUND);
        }

        public async Task<TableViewModel<Expense>> GetExpenseList(string userId, int page, int limit, string keyword, string order)
        {
            IQueryable<Expense> query = _dbContext.TExpense.AsQueryable()
                .Where(x => x.UserId == userId && (
                    x.Name.Contains(keyword)
                ));
            int totalData = query.Count();

            PaginationViewModel paginationView = new PaginationViewModel(
                page, 
                limit, 
                totalData, 
                keyword, 
                $"{baseUrl}/expense");
            paginationView.Order = order;

            query = order switch
            {
                OrderConstants.ORDER_BY_DATE_ASC => query.OrderBy(x => x.CreatedAt),
                _ => query.OrderByDescending(x => x.CreatedAt),
            };
            List<Expense> userExpenses = await query
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

        public async Task InsertFromExcel(string userId, IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets[0];

                    int i = 2;
                    
                    while (!string.IsNullOrWhiteSpace((string)worksheet.Cells[i, 1].Value))
                    {
                        if (DateTime.TryParseExact(
                            worksheet.Cells[i, 1].Value.ToString(),
                            "dd/MM/yyyy HH:mm:ss",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime creationDate
                        )) {
                            Expense expense = new Expense();
                            expense.UserId = userId;
                            expense.CreatedAt = creationDate;
                            expense.UpdatedAt = creationDate;
                            expense.Name = worksheet.Cells[i, 2].Value.ToString();
                            expense.Cost = long.Parse(worksheet.Cells[i, 3].Value.ToString());
                            expense.CostType = CostTypeEnum.MISC;
                            _dbContext.TExpense.Add(expense);
                        }
                        i++;
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<List<PlotDataModel<DateTime, long>>> GetCostByDate(string userId)
        {
            var costs = await _dbContext.TExpense
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.CreatedAt.Date)
                .Select(x => new PlotDataModel<DateTime, long>(
                    x.Key,
                    x.Sum(y => y.Cost)
                ))
                .ToListAsync();

            return costs;
        }
    }
}