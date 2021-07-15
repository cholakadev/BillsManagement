namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.DAL.Settings;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly SecuritySettings _securitySettings;

        public UserService(IUserRepository repository, IOptions<SecuritySettings> securitySettings)
        {
            this._repository = repository;
            this._securitySettings = securitySettings.Value;
        }

        public string Login(string email, string password)
        {
            if (!this._repository.IsExistingUser(email))
            {
                throw new Exception("Authentication failed.");
            }

            var userAuth = this._repository.GetUserEncryptedPasswordByEmail(email);
            DecryptCriteria criteria = new DecryptCriteria()
            {
                Email = userAuth.Email,
                Password = userAuth.EncrypedPassword,
                Secret = "b14ca5898a4e4133bbce2ea2315a1916" //this._securitySettings.EncryptionKey
            };
            var decodedPassword = PasswordCipher.DecryptString(criteria.Secret, userAuth.EncrypedPassword);
            if (password != decodedPassword)
            {
                throw new Exception("Authentication failed.");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userAuth.UserId.ToString())
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

        public User Register(User user, string password)
        {
            if (this._repository.IsExistingUser(user.Email))
            {
                throw new Exception("User already exists.");
            }

            user.UserId = Guid.NewGuid();
            var criteria = new RegisterCriteria();
            criteria.User = user;

            var encryptCriteria = new EncryptCriteria()
            {
                Email = user.Email,
                Password = password,
                Secret = "b14ca5898a4e4133bbce2ea2315a1916" //this._securitySettings.EncryptionKey
            };

            criteria.Password = PasswordCipher.EncryptString(encryptCriteria.Secret, encryptCriteria.Password);

            var response = this._repository.Register(criteria);

            return response;
        }
    }
}
