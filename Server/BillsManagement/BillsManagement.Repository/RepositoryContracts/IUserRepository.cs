namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.Models;
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<Object> Register(object user);

        bool IsExistingUser(string email);

        User GetUserInformation(SearchCriteria getUserInformationSearchCriteria);
    }
}
