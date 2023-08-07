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

namespace MoneyFlow.Services
{
    public class MotivationService
    {
        private readonly DatabaseContext _dbContext;

        private readonly static string baseUrl = Startup.StaticConfiguration.GetSection("BASE_URL").Value;

        public MotivationService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TableViewModel<Motivation>> GetMotivations(string userId, int page, int limit, string keyword)
        {
            int totalData = _dbContext.TMotivation
                .Where(x => x.UserId == userId && (
                    x.TargetName.Contains(keyword)
                    || x.TargetPrice.ToString().Contains(keyword)
                    || x.Description.Contains(keyword)
                ))
                .Count();

            PaginationViewModel paginationView = new PaginationViewModel(
                page,
                limit,
                totalData,
                keyword,
                $"{baseUrl}/motivations"
            );

            List<Motivation> motivations = await _dbContext.TMotivation
                .Where(x => x.UserId == userId && (
                    x.TargetName.Contains(keyword)
                    || x.TargetPrice.ToString().Contains(keyword)
                    || x.Description.Contains(keyword)
                ))
                .ToListAsync();

            return new TableViewModel<Motivation>(
                motivations,
                paginationView
            );
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
    }
}