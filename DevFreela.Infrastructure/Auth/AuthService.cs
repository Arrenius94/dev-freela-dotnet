using DevFreela.Core.Entities;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(string email, EUserRole role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(key)) throw new Exception("JWT Key is not configured.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("userName", email),
                new Claim(ClaimTypes.Role, role.ToString())
            }; 

            var token = new JwtSecurityToken(
                issuer: issuer, 
                audience: audience, 
                expires: DateTime.Now.AddHours(8), 
                signingCredentials: credentials,
                claims: claims
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        public string ComputeSha256Hash(string password)
        {
            using (SHA256 sha526Hash = SHA256.Create())
            {
                byte[] bytes = sha526Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
