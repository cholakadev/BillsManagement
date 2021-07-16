namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DomainModels.User;

    public interface IUserService
    {
        RegisterResponse Register(RegisterRequest request);

        LoginResponse Login(LoginRequest request);
    }
}
