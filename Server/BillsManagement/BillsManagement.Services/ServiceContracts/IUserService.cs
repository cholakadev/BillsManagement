namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DAL.Models;
    using BillsManagement.DomainModels.User;

    public interface IUserService
    {
        User Register(User user, string password);

        LoginResponse Login(LoginRequest request);
    }
}
