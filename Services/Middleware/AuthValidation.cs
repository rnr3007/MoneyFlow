using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MoneyFlow.Constants;
using MoneyFlow.Data;
using MoneyFlow.Utils;

namespace MoneyFlow.Services.Middleware
{
    public class AuthValidation
    {
        private readonly RequestDelegate _next;

        public AuthValidation(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext _context, DatabaseContext _dbContext)
        {
            string requestPath = _context.Request.Path;
            string loginPath = "/user/login";
            try {
                var jwt = _context.Request.Cookies["TokenBearer"];
                string id = AuthUtilites.ValidateJwt(jwt);
                
                var user = _dbContext.TUser.Where(x => x.Id == id).FirstOrDefault() 
                    ?? throw new DataException(ErrorMessage.USER_NOT_FOUND);

                _context.Response.Cookies.Append("TokenBearer", jwt, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(1)
                });
                _context.Request.Headers.Add("userId", user.Id);
                if (requestPath == loginPath)
                {
                    _context.Response.Redirect("/");
                }
                return _next.Invoke(_context);
            } catch (Exception e)
            {
                Type exceptionType = e.GetType();
                if (!(exceptionType == typeof(DataException) || exceptionType == typeof(SecurityTokenException)))
                {
                    Console.WriteLine(e);
                }
                _context.Response.Cookies.Delete("TokenBearer");
                if (requestPath != loginPath) { _context.Response.Redirect("/user/login"); }
                return _next.Invoke(_context);
            }
        }
    }
}