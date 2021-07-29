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

    public partial class UserService : BaseService, IUserService
    {
        private readonly SecuritySettings _securitySettings;

        public UserService(IUserRepository repository, IOptions<SecuritySettings> securitySettings)
            : base(repository)
        {
            this._securitySettings = securitySettings.Value;
        }

        public LoginResponse Login(LoginRequest request)
        {
            DomainModel.Authentication auth = this._userRepository
                .GetUserEncryptedPasswordByEmail(request.Email);

            DomainModel.SecurityToken token = this._userRepository
                .GetSecurityTokenByUserId(auth.UserId);

            PasswordCipher.Decrypt(auth.Password, request.Password);

            LoginResponse response = new LoginResponse();

            response.Token = (token == null || token.IsExpired == true) ?
                              this.GenerateJwtToken(auth, request.Email) :
                              token.SecurityToken1;

            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            if (this._userRepository.IsExistingUser(request.Email))
            {
                throw new Exception("Email is already taken.");
            }

            var encryptedPassword = PasswordCipher.Encrypt(request.Password);

            var registration = this._userRepository
                .Register(request.Email, encryptedPassword, out DomainModel.Settings settings);

            this.SendRegisterNotificationOnEmail(registration, settings);

            RegisterResponse response = new RegisterResponse();
            response.Registration = registration;
            return response;
        }
    }
}
