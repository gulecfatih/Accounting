using AccountingWebApi.Data.Auth;
using AccountingWebApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AccountingWebApi.Business
{
    public class JwtLoginLogic
    {

        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;

        public JwtLoginLogic(IConfiguration configuration,IDistributedCache cache)
        {
            _configuration = configuration;
            _cache = cache;
        }

        /// <summary>
        ///  Token İle Kullanıcının UserId'sini Çeker
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string TokenWithUserId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            if (userId != null)
            {
                return userId;
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// Token ile Kullanıcının Role nu Çeker
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string TokenWithRole(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            if (role != null)
            {
                return role;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Token Üretir
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string GenerateToken(string userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role)
            }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// JWT tokeni onaylayıp Cacheleme işlemini gerçekleştirir
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        public void ValidationToken(string userId, string token)
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(240),
                AbsoluteExpiration = DateTime.Now.AddHours(2),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };
             _cache.SetStringAsync(userId, token, cacheEntryOptions);
        }


    }
}
