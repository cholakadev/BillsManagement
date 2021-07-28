﻿namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DAL.Models;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public partial class UserService : IUserService
    {
        public static string Issuer { get; } = Guid.NewGuid().ToString();
        public static DateTime Expires { get; private set; } = DateTime.UtcNow.AddMinutes(2);

        private string GenerateJwtToken(Authentication auth, DecryptCriteria criteria, string email)
        {
            var tokenGenerateTime = DateTime.Now;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", auth.UserId.ToString()),
                    new Claim("Email", email),
                    new Claim("SecretGuid", Guid.NewGuid().ToString()),
                    new Claim("GenerateDate", tokenGenerateTime.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                     Encoding.UTF8
                     .GetBytes(criteria.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, tokenDescriptor.Subject.Claims, null, Expires, tokenDescriptor.SigningCredentials));
            //var token = tokenHandler.WriteToken(securityToken);
            return securityToken;
        }
    }
}
