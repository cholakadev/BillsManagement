namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.DAL.Settings;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.Extensions.Options;
    using System;

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly SecuritySettings _securitySettings;

        public UserService(IUserRepository repository, IOptions<SecuritySettings> securitySettings)
        {
            this._repository = repository;
            this._securitySettings = securitySettings.Value;
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
                Secret = this._securitySettings.EncryptionKey
            };

            criteria.Password = PasswordCipher.Encode(encryptCriteria);

            var response = this._repository.Register(criteria);

            return response;
        }
    }
}
