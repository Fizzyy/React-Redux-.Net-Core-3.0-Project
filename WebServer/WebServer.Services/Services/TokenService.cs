using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebServer.Services.Services
{
    public class TokenService
    {
        public static object CreateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
            };
            return response;
        }

        public static List<Claim> GetClaims(string token)
        {
            if (token == null)
            {
                return null;
            }
            List<Claim> claims = new List<Claim>();
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;

            if (tokenS != null)
                foreach (var item in tokenS.Claims)
                {
                    claims.Add(item);
                }

            return claims;
        }

        public static bool IsExpired(string token)
        {
            TokenValidationParameters validationParameters =
                new TokenValidationParameters
                {
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidIssuer = AuthOptions.ISSUER,
                    IssuerSigningKeys = new[] { new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOptions.KEY)) }
                };

            var validate = new JwtSecurityTokenHandler();
            try
            {
                validate.ValidateToken(token, validationParameters, out _);
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }

        public static List<Claim> VerifyToken(string token)
        {
            if (!IsExpired(token))
            {
                var tokenClaims = GetClaims(token);
                if (tokenClaims != null)
                    return tokenClaims;
            }
            return null;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}