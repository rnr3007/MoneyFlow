using System;
using System.Data;
using System.Linq;
using MoneyFlow.Constants;
using MoneyFlow.Context;
using MoneyFlow.Models;
using MoneyFlow.Utils;
using iv = mvc.Utils.InputValidator;

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
            if (!iv.IsUsernameValid(user.Username)){ throw new FormatException(ErrorMessage.USERNAME_FORMAT_INVALID); }
            if (!iv.IsPasswordValid(user.Password)){ throw new FormatException(ErrorMessage.PASSWORD_FORMAT_INVALID); }
            if (!iv.IsFullnameValid(user.FullName)){ throw new FormatException(ErrorMessage.FULLNAME_FORMAT_INVALID); }
            if (!iv.IsEmailValid(user.Email)){ throw new FormatException(ErrorMessage.EMAIL_FORMAT_INVALID); }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            var jwtToken = AuthUtilites.GenerateJwt(user);
            _context.SaveChanges();
            return jwtToken;
        }

        public string Login(User user)
        {
            if (!iv.IsNotEmpty(user.Username) || !iv.IsNotEmpty(user.Password)) { throw new FormatException(ErrorMessage.FIELD_EMPTY); }
            var userResult = _context.Users.Where(x => x.Username == user.Username).FirstOrDefault() ?? throw new DataException(ErrorMessage.USER_NOT_FOUND);
            if (!BCrypt.Net.BCrypt.Verify(user.Password, userResult.Password)) { throw new DataException(ErrorMessage.WRONG_PASSWORD);}
            var jwtToken = AuthUtilites.GenerateJwt(userResult);
            return jwtToken;
        }

        public string Edit(User user)
        {
            if (!iv.IsUsernameValid(user.Username)){ throw new FormatException(ErrorMessage.USERNAME_FORMAT_INVALID); }
            if (!iv.IsPasswordValid(user.Password)){ throw new FormatException(ErrorMessage.PASSWORD_FORMAT_INVALID); }
            if (!iv.IsFullnameValid(user.FullName)){ throw new FormatException(ErrorMessage.FULLNAME_FORMAT_INVALID); }
            if (!iv.IsEmailValid(user.Email)){ throw new FormatException(ErrorMessage.EMAIL_FORMAT_INVALID); }
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