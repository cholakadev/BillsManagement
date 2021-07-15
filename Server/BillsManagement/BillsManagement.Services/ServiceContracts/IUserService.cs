namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DAL.Models;

    public interface IUserService
    {
        User Register(User user, string password);

        string Login(string email, string password);
    }
}
