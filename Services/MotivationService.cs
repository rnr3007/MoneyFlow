using System.Threading.Tasks;
using MoneyFlow.Data;
using MoneyFlow.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using fu = MoneyFlow.Utils.FileUtilites;
using MoneyFlow.Constants;
using System.Data;
using MoneyFlow.Models.DetailComponents;
using Microsoft.Data.SqlClient;

namespace MoneyFlow.Services
{
    public class MotivationService
    {
        private readonly DatabaseContext _dbContext;

        public MotivationService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TableView<Motivation>> GetMotivations(string userId, int page, int limit, string keyword)
        {
            IQueryable<Motivation> query = _dbContext.TMotivation
                .Where(x => x.UserId == userId && (
                    x.TargetName.Contains(keyword)
                    || x.TargetPrice.ToString().Contains(keyword)
                    || x.Description.Contains(keyword)
                ));
            int totalData = query.Count();

            Pagination paginationView = new Pagination(
                page,
                limit,
                totalData
            );

            List<Motivation> motivations = await query
                .Skip(paginationView.LimitData * (paginationView.ChoosenPage - 1))
                .Take(paginationView.LimitData)
                .ToListAsync();

            TableView<Motivation> tableView = new TableView<Motivation>(
                motivations,
                paginationView
            );

            tableView.SearchBarView = new SearchBar(
                keyword
            );

            return tableView;
        }
    
        public async Task CreateMotivations(string userId, Motivation motivation, IFormFile formFile)
        {
            motivation.UserId = userId;
            _dbContext.TMotivation.Add(motivation);
            motivation.TargetImage = await fu.SaveFile(
                GeneralConstants.PURPOSE_MOTIVATIONS,
                formFile,
                userId
            );
            await _dbContext.SaveChangesAsync();
        }
    
        public async Task<Motivation> GetMotivation(string userId, string motivationId)
        {
            return await _dbContext.TMotivation
                .Where(x => x.UserId == userId && x.Id == motivationId)
                .FirstOrDefaultAsync() ?? throw new DataException(ErrorMessage.MOTIVATION_NOT_FOUND);
        }

        public async Task UpdateMotivation(string userId, string motivationId, Motivation motivation, IFormFile formFile)
        {
            Motivation motivationResult = await GetMotivation(userId, motivationId);

            motivationResult.TargetName = motivation.TargetName;
            motivationResult.TargetPrice = motivation.TargetPrice;
            motivationResult.Description = motivation.Description;

            await fu.UpdateFile(
                GeneralConstants.PURPOSE_MOTIVATIONS,
                formFile,
                motivationResult.TargetImage,
                userId
            );

            await _dbContext.SaveChangesAsync();
        }
    
        public async Task DeleteMotivation(string userId, string motivationId)
        {
            Motivation motivation = await GetMotivation(userId, motivationId);
            _dbContext.TMotivation.Remove(motivation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task BuyMotivation(string userId, string motivationId)
        {
            SqlParameter[] parameters = new[]{
                new SqlParameter("@MotivationId", motivationId),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ReceiptFile", null ?? "")
            };

            await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC BuyDream @MotivationId, @UserId, @ReceiptFile",
                parameters
            );
        }
    }
}