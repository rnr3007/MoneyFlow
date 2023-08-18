using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using MoneyFlow.Constants;
using MoneyFlow.Data;

namespace MoneyFlow.Utils
{
    public class AuthUtilites
    {
        private static readonly string jwtKey = Startup.StaticConfiguration.GetSection("JWT_KEY").Value;
        private static readonly string jwtIssuer = Startup.StaticConfiguration.GetSection("JWT_ISSUER").Value;
        public static string GenerateJwt(User user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );
            SigningCredentials credentials = new SigningCredentials(
                securityKey, 
                SecurityAlgorithms.HmacSha256
            );

            List<Claim> claims = new List<Claim>
            {
                new Claim("id", user.Id)
            };
            
            JwtSecurityToken token = new JwtSecurityToken(
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