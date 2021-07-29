﻿namespace BillsManagement.Services.Services.UserService
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

        public UserService(IUserRepository userRepository, IAuthenticationRepository authenticationRepository, IOptions<SecuritySettings> securitySettings)
            : base(userRepository, authenticationRepository)
        {
            this._securitySettings = securitySettings.Value;
        }

        public LoginResponse Login(LoginRequest request)
        {
            DomainModel.Authentication auth = this._userRepository
                .GetUserEncryptedPasswordByEmail(request.Email);

            PasswordCipher.Decrypt(auth.Password, request.Password);

            DomainModel.SecurityToken token = this._userRepository
                .GetSecurityTokenByUserId(auth.UserId);

            DomainModel.TokenValidator tokenValidator = new DomainModel.TokenValidator()
            {
                SecurityToken = token,
                Authentication = auth,
                Email = request.Email
            };

            var securityToken = this.GetValidatedToken(tokenValidator);

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
    }
}
