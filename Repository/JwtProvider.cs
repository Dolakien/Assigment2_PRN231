using BusinessObject.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOption _options;

        public JwtProvider(IOptions<JwtOption> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(BranchAccount account)
        {
            // Kiểm tra điều kiện đầu vào
            if (account == null)
            {
                throw new Exception("Account cannot be null.");
            }

            if (string.IsNullOrEmpty(account.EmailAddress) || account.Role == null || string.IsNullOrEmpty(account.Role.RoleName))
            {
                throw new Exception("Account email or role information is missing.");
            }


            // Tạo claims
            var claims = new Claim[]
            {
        new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
        new Claim(ClaimTypes.Email, account.EmailAddress),
        new Claim(ClaimTypes.Role, account.Role.RoleName)
            };

            // Tạo credentials cho chữ ký
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha512Signature);

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_options.ExpiryTimeFrame),
                SigningCredentials = signingCredentials
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(token);
                var tokenResult = tokenHandler.WriteToken(securityToken);
                return tokenResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while generating the token: " + ex.Message);
            }
        }


    }
}
