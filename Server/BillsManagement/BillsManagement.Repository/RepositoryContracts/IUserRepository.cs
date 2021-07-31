namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using System;

    public interface IUserRepository : IBaseRepository<User>
    {
        DomainModel.Registration Register(string email, string password, out DomainModel.Settings settings);

        bool IsExistingUser(string email);

        DomainModel.User GetUserDetails(string email);

        DomainModel.Authorization GetAuthorizationByUserId(Guid userId);

        Guid GetUserInformation(string email);

        void UpdateToken(DomainModel.Authorization token);
    }
}
