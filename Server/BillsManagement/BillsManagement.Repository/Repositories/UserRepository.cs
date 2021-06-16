namespace BillsManagement.Repository.Repositories
{
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        public User GetUserInformation(SearchCriteria getUserInformationSearchCriteria)
        {
            throw new NotImplementedException();
        }

        public bool IsExistingUser(string email)
        {
            throw new NotImplementedException();
        }

        public Task<object> Register(object user)
        {
            throw new NotImplementedException();
        }
    }
}
