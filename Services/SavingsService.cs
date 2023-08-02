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
    }
}