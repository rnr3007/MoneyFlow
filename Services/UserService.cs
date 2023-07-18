using System;
using System.Data;
using System.Linq;
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

        public UserService(UserContext context)
        {
            _context = context;
        }

        public string Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            var jwtToken = AuthUtilites.GenerateJwt(user);
            _context.SaveChanges();
        }

        public string Login(User user)
        {
            var userResult = _context.Users.Where(x => x.Username == user.Username).FirstOrDefault() ?? throw new DataException(ErrorMessage.USER_NOT_FOUND);
            if (!BCrypt.Net.BCrypt.Verify(user.Password, userResult.Password)) { throw new DataException(ErrorMessage.WRONG_PASSWORD);}
            var jwtToken = AuthUtilites.GenerateJwt(userResult);
            
            return jwtToken;
        }

        public string Edit(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Update(user);
            var jwtToken = AuthUtilites.GenerateJwt(user);
            return jwtToken;
        }

        public void Delete(User user)
        {
            _context.Remove(user);
        }
    }
}