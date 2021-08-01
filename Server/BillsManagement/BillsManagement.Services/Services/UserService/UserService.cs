namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DomainModel;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.Extensions.Options;
    using System;

    public partial class UserService : IUserService
    {
        private readonly Secrets _secrets;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;

        public UserService(IOptions<Secrets> secrets, IUserRepository userRepository, IAuthorizationRepository authorizationRepository)
        {
            this._secrets = secrets.Value ?? throw new ArgumentException(nameof(secrets));
            this._userRepository = userRepository;
            this._authorizationRepository = authorizationRepository;
        }

        public LoginResponse Login(LoginRequest request)
        {
            DomainModel.User user = this._userRepository
                .GetUserDetails(request.Email);

            PasswordCipher.Decrypt(user.Password, request.Password);

            DomainModel.Authorization token = this._userRepository
                .GetAuthorizationByUserId(user.UserId);

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
            DomainModel.Authorization authorization = this._userRepository.GetAuthorizationByUserId(userId);

            if (authorization.ExpirationDate <= DateTime.Now)
            {
                throw new Exception("Unauthorized");
            }
        }
    }
}
