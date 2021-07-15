using BillsManagement.DAL.EntityModels;
using BillsManagement.Security;
using BillsManagement.Services.ServiceContracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BillsManagement.Services.Services.UserService
{
    public partial class UserService : IUserService
    {
        private string GenerateJwtToken(UserAuthentication userAuth, DecryptCriteria criteria)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userAuth.UserId.ToString()),
                    new Claim("Email", userAuth.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                     Encoding.UTF8
                     .GetBytes(criteria.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
