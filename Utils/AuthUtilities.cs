using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoneyFlow.Models;

namespace MoneyFlow.Utils
{
    public class AuthUtilites
    {
        public static string GenerateJwt(User user)
        {
            string jwtKey = Startup.StaticConfiguration.GetSection("JWT_KEY").ToString();
            string jwtIssuer = Startup.StaticConfiguration.GetSection("JWT_ISSUER").ToString();

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );
            var credentials = new SigningCredentials(
                securityKey, 
                SecurityAlgorithms.HmacSha256
            );
            var claims = new[]
            {
                new Claim("id", user.Id)
            };
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                claims: claims,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}