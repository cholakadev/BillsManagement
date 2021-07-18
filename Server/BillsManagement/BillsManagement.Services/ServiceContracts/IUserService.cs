namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DomainModel.User;

    public interface IUserService
    {
        RegisterResponse Register(RegisterRequest request);

        LoginResponse Login(LoginRequest request);
    }
}
