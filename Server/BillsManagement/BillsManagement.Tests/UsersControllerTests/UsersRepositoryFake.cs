using BillsManagement.DAL.Models;
using BillsManagement.DomainModel;
using BillsManagement.Repository.RepositoryContracts;

namespace BillsManagement.Tests.UsersControllerTests
{
    public class UsersRepositoryFake : IUserRepository
    {
        public Authentication GetUserEncryptedPasswordByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistingUser(string email)
        {
            throw new System.NotImplementedException();
        }

        public Registration Register(Registration registrationRequest, string password)
        {
            throw new System.NotImplementedException();
        }

        public Registration Register(string email, string password, out Settings settings)
        {
            throw new System.NotImplementedException();
        }
    }
}
