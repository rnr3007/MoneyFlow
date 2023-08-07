using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Data;
using OfficeOpenXml;

namespace MoneyFlow.Services
{
    public class FileService
    {
        private readonly DatabaseContext _dbContext;

        private static IWebHostEnvironment _environment;

        public FileService(DatabaseContext dbContext, IWebHostEnvironment environment)
        {
            _environment = environment;
            _dbContext = dbContext;
        }

        public async Task<byte[]> RetrieveExcel(string userId, string purpose)
        {
            string fileName = $"{DateTime.UtcNow.ToString("yyyy-mm-ddTHH:mm:ssZ")}_{userId}_{purpose}.xlsx";
            string filePath = Path.Combine(_environment.ContentRootPath, "UserData", "tempExcel", fileName);
            using (var fileStream = File.Create(filePath))
            {
                using (var excelPackage = new ExcelPackage(fileStream))
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add("Expenses");
                    sheet.Cells["A1"].Value = "Tanggal pengeluaran";
                    sheet.Cells["B1"].Value = "Nama pengeluaran";
                    sheet.Cells["C1"].Value = "Jumlah pengeluaran";

                    List<Expense> expenses = await _dbContext.TExpense
                        .Where(x => x.UserId == userId)
                        .ToListAsync();

                    for (int i = 0; i < expenses.Count(); i++)
                    {
                        sheet.Cells[i+2, 1].Value = expenses[i].CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ");
                        sheet.Cells[i+2, 2].Value = expenses[i].Name;
                        sheet.Cells[i+2, 3].Value = expenses[i].Cost;
                    }
                    return excelPackage.GetAsByteArray();
                }
            }
        }
    }
    
}