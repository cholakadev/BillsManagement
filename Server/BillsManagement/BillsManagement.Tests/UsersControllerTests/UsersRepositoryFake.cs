using BillsManagement.DomainModel;
using System;

namespace BillsManagement.Tests.UsersControllerTests
{
    public class UsersRepositoryFake //: IUserRepository
    {
        public DomainModel.SecurityToken GetSecurityTokenByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public DomainModel.User GetUserEncryptedPasswordByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Guid GetUserInformation(string email)
        {
            throw new NotImplementedException();
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

        public void UpdateToken(DomainModel.SecurityToken token)
        {
            throw new NotImplementedException();
        }
    }
}
