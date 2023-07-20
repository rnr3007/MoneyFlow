using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoneyFlow.Constants;
using MoneyFlow.Context;
using MoneyFlow.Models;
using MoneyFlow.Utils;

namespace MoneyFlow.Middleware
{
    public class AuthValidation
    {
        private readonly RequestDelegate _next;

        public AuthValidation(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext _context, UserContext _dbContext)
        {
            try {
                var jwt = _context.Request.Cookies["TokenBearer"];
                string id = AuthUtilites.ValidateJwt(jwt);
                
                var user = _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync() 
                    ?? throw new DataException(ErrorMessage.USER_NOT_FOUND);

                _context.Response.Cookies.Append("TokenBearer", id, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                _context.Response.Cookies.Append("Id", id);
                await _next.Invoke(_context);
            } catch (DataException)
            {
                _context.Response.Cookies.Delete("TokenBearer");
                _context.Response.Cookies.Delete("Id");
                _context.Response.Redirect("/user/login");
                return;
            } catch (SecurityTokenException)
            {
                _context.Response.Cookies.Delete("TokenBearer");
                _context.Response.Cookies.Delete("Id");
                _context.Response.Redirect("/user/login");
                return;
            } catch (Exception e)
            {
                Console.WriteLine(e);
                _context.Response.Cookies.Delete("TokenBearer");
                _context.Response.Cookies.Delete("Id");
                _context.Response.Redirect("/user/login");
                return;
            }
        }
    }
}