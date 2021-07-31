namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DomainModel;
    using System;

    public interface IUserService
    {
        DomainModel.RegisterResponse Register(RegisterRequest request);

        DomainModel.LoginResponse Login(LoginRequest request);

        void ValidateJwtToken(Guid userId);
    }
}
