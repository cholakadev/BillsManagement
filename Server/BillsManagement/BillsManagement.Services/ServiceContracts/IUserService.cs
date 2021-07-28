namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DomainModel.User;

    public interface IUserService
    {
        DomainModel.RegisterResponse Register(RegisterRequest request);

        DomainModel.LoginResponse Login(LoginRequest request);
    }
}
