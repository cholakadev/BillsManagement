namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DomainModel;
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
            DomainModel.User user = this._userRepository
                .GetUserDetails(request.Email);

            PasswordCipher.Decrypt(user.Password, request.Password);

            DomainModel.SecurityToken token = this._userRepository
                .GetSecurityTokenByUserId(user.UserId);

            DomainModel.TokenValidator tokenValidator = new DomainModel.TokenValidator();
            tokenValidator.SecurityToken = token;
            tokenValidator.User = user;

            var securityToken = this.GetValidToken(tokenValidator);

            DomainModel.LoginResponse response = new DomainModel.LoginResponse();
            response.Token = securityToken;

            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            this.ValidateUserExistence(request.Email);
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
