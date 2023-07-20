using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MoneyFlow.Constants;
using MoneyFlow.Models;

namespace MoneyFlow.Utils
{
    public class AuthUtilites
    {
        private static readonly string jwtKey = Startup.StaticConfiguration.GetSection("JWT_KEY").Value;
        private static readonly string jwtIssuer = Startup.StaticConfiguration.GetSection("JWT_ISSUER").Value;
        public static string GenerateJwt(User user)
        {
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
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(1)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string ValidateJwt(string jwt)
        {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(jwt, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
                    ValidIssuer = jwtIssuer,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var validToken = (JwtSecurityToken)validatedToken;
                string id = validToken.Claims.First(j => j.Type == "id").Value;
                return id;
            } catch (Exception)
            {
                throw new SecurityTokenException(ErrorMessage.INVALID_TOKEN);
            }
        }
    }
}