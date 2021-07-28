namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DAL.Settings;
    using BillsManagement.DomainModel;
    using BillsManagement.DomainModel.User;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.Extensions.Options;
    using System;

    public partial class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly SecuritySettings _securitySettings;

        public UserService(IUserRepository repository, IOptions<SecuritySettings> securitySettings)
        {
            this._repository = repository;
            this._securitySettings = securitySettings.Value;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var auth = this._repository.GetUserEncryptedPasswordByEmail(request.Email);
            var token = this._repository.GetSecurityTokenByUserId(auth.UserId);

            var criteria = new DecryptCriteria() { Password = auth.Password };

            LoginResponse response = new LoginResponse();

            if (token == null || token.IsExpired == true)
            {
                criteria.Secret = this._securitySettings.JWT_Secret;
                response.Token = this.GenerateJwtToken(auth, criteria, request.Email);
            }

            if (token != null && token.IsExpired == false)
            {
                criteria.Secret = token.Secret;
                response.Token = this.GenerateJwtToken(auth, criteria, request.Email);
            }

            if (request.Password != PasswordCipher.Decrypt(criteria) || auth == null)
            {
                throw new Exception("Authentication failed.");
            }

            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            if (this._repository.IsExistingUser(request.Email))
            {
                throw new Exception("Email is already taken.");
            }

            var encryptCriteria = new EncryptCriteria()
            {
                Password = request.Password,
                Secret = this._securitySettings.JWT_Secret
            };

            var encryptedPassword = PasswordCipher.Encrypt(encryptCriteria);

            var registration = this._repository.Register(request.Email, encryptedPassword, out DomainModel.Settings settings);

            this.SendRegisterNotificationOnEmail(registration, settings);

            RegisterResponse response = new RegisterResponse();
            response.Registration = registration;
            return response;
        }
    }
}
