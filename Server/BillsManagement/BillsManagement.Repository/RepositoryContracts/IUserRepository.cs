namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using System;

    public interface IUserRepository : IBaseRepository<User>
    {
        DomainModel.Registration Register(string email, string password, out DomainModel.Settings settings);

        bool IsExistingUser(string email);

        DomainModel.Authentication GetUserEncryptedPasswordByEmail(string email);

        DomainModel.SecurityToken GetSecurityTokenByUserId(Guid userId);

        Guid GetUserInformation(string email);

        void UpdateToken(DomainModel.SecurityToken token);
    }
}
