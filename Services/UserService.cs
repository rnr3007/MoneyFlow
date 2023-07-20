using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MoneyFlow.Constants;
using MoneyFlow.Context;
using MoneyFlow.Models;
using MoneyFlow.Utils;
using iv = MoneyFlow.Utils.InputValidator;

namespace MoneyFlow.Services
{
    public class UserService
    {
        private readonly UserContext _context;

        private readonly IHttpContextAccessor _httpContext;

        private HttpContext HttpContext => _httpContext.HttpContext;

        public UserService(UserContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            var jwtToken = AuthUtilites.GenerateJwt(user);
            await _context.SaveChangesAsync();
        }

        public async Task Login(User user)
        {
            var userResult = _context.Users.Where(x => x.Username == user.Username).FirstOrDefault() ?? throw new DataException(ErrorMessage.USER_NOT_FOUND);
            if (!BCrypt.Net.BCrypt.Verify(user.Password, userResult.Password)) { throw new DataException(ErrorMessage.WRONG_PASSWORD);}
            var jwtToken = AuthUtilites.GenerateJwt(userResult) ?? throw new Exception(ErrorMessage.SERVER_ERROR);
            HttpContext.Response.Cookies.Append("TokenBearer", jwtToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }

        public void Edit(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Update(user);
            var jwtToken = AuthUtilites.GenerateJwt(user);
        }

        public void Delete(User user)
        {
            _context.Remove(user);
        }
    }
}