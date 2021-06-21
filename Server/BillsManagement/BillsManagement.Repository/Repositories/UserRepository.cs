namespace BillsManagement.Repository.Repositories
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System;

    public class UserRepository : IUserRepository
    {
        private readonly BillsManagementContext _dbContext;

        public UserRepository(BillsManagementContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public User GetUserInformation(Criteria getUserInformationSearchCriteria)
        {
            throw new NotImplementedException();
        }

        public bool IsExistingUser(string email)
        {
            throw new NotImplementedException();
        }

        public User Register(RegisterCriteria criteria)
        {
            if (criteria != null && criteria.Password != String.Empty)
            {
                this._dbContext.Users.Add(criteria.User);
                this._dbContext.SaveChanges();
            }

            return criteria.User;
        }
    }
}
