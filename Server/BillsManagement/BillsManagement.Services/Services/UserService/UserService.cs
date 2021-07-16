namespace BillsManagement.Services.Services.UserService
{
    using AutoMapper;
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.DAL.Settings;
    using BillsManagement.DomainModels.User;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.Extensions.Options;
    using System;

    public partial class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly SecuritySettings _securitySettings;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IOptions<SecuritySettings> securitySettings, IMapper mapper)
        {
            this._repository = repository;
            this._securitySettings = securitySettings.Value;
            this._mapper = mapper;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var userAuth = this._repository.GetUserEncryptedPasswordByEmail(request.Email);

            var criteria = new DecryptCriteria()
            {
                Password = userAuth.EncrypedPassword,
                Secret = this._securitySettings.EncryptionKey
            };

            if (request.Password != PasswordCipher.Decrypt(criteria) || userAuth == null)
            {
                throw new Exception("Authentication failed.");
            }

            LoginResponse response = new LoginResponse();
            response.Token = this.GenerateJwtToken(userAuth, criteria);
            return response;
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
