namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.DAL.Settings;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using Microsoft.Extensions.Options;
    using System;

    public partial class UserService// : IUserService
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
            var userAuth = this._repository.GetUserEncryptedPasswordByEmail(email);

            var criteria = new DecryptCriteria()
            {
                Password = userAuth.EncrypedPassword,
                Secret = this._securitySettings.EncryptionKey
            };

            if (password != PasswordCipher.Decrypt(criteria) || userAuth == null)
            {
                throw new Exception("Authentication failed.");
            }

            var token = this.GenerateJwtToken(userAuth, criteria);
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
                Password = password,
                Secret = this._securitySettings.EncryptionKey
            };

            criteria.Password = PasswordCipher.Encrypt(encryptCriteria);

            var response = this._repository.Register(criteria);

            return response;
        }
    }
}
