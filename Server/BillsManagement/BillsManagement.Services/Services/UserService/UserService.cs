namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DomainModel;
    using BillsManagement.DomainModel.User;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using System;

    public partial class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public UserService(IUserRepository userRepository, IAuthenticationRepository authenticationRepository)
        {
            this._userRepository = userRepository;
            this._authenticationRepository = authenticationRepository;
        }

        public LoginResponse Login(LoginRequest request)
        {
            DomainModel.Authentication auth = this._userRepository
                .GetUserEncryptedPasswordByEmail(request.Email);

            PasswordCipher.Decrypt(auth.Password, request.Password);

            DomainModel.SecurityToken token = this._userRepository
                .GetSecurityTokenByUserId(auth.UserId);

            auth.Email = request.Email;

            DomainModel.TokenValidator tokenValidator = new DomainModel.TokenValidator();
            tokenValidator.SecurityToken = token;
            tokenValidator.Authentication = auth;

            var securityToken = this.GetValidToken(tokenValidator);

            DomainModel.LoginResponse response = new DomainModel.LoginResponse();
            response.Token = securityToken;

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

            this.SendRegisterNotificationEmail(registration, settings);

            RegisterResponse response = new RegisterResponse();
            response.Registration = registration;
            return response;
        }

        public void ValidateJwtToken(Guid userId)
        {
            DomainModel.SecurityToken token = this._userRepository.GetSecurityTokenByUserId(userId);

            if (token.ExpirationDate <= DateTime.Now)
            {
                throw new Exception("Unauthorized");
            }
        }
    }
}
