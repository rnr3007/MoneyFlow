using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Constants;
using MoneyFlow.Data;
using MoneyFlow.Utils;

namespace MoneyFlow.Services
{
    public class UserService
    {
        private readonly DatabaseContext _dbContext;

        private readonly IHttpContextAccessor _httpContext;

        private HttpContext HttpContext => _httpContext.HttpContext;

        public UserService(DatabaseContext context, IHttpContextAccessor httpContext)
        {
            _dbContext = context;
            _httpContext = httpContext;
        }

        public async Task Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _dbContext.TUser.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Login(User user)
        {
            var userResult = await _dbContext.TUser.Where(x => x.Username == user.Username).FirstOrDefaultAsync() 
                ?? throw new DataException(ErrorMessage.USER_NOT_FOUND);
            if (!BCrypt.Net.BCrypt.Verify(user.Password, userResult.Password)) { throw new DataException(ErrorMessage.WRONG_PASSWORD);}
            var jwtToken = AuthUtilites.GenerateJwt(userResult) ?? throw new Exception(ErrorMessage.SERVER_ERROR);
            HttpContext.Response.Cookies.Append("TokenBearer", jwtToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(1)
            });
        }

        public async Task<User> GetUser(string userId)
        {
            return await _dbContext.TUser.Where(x => x.Id == userId).FirstOrDefaultAsync();
        }
    }
}