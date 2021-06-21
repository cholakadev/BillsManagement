namespace BillsManagement.Repository.Repositories
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System;
    using System.Linq;

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
            User user = this._dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public User Register(RegisterCriteria criteria)
        {
            if (criteria != null && criteria.Password != String.Empty)
            {
                this._dbContext.Users.Add(criteria.User);

                var authentication = new Authentication()
                {
                    AuthenticationId = Guid.NewGuid(),
                    Password = criteria.Password,
                    UserId = criteria.User.UserId
                };

                this._dbContext.Authentications.Add(authentication);
                this._dbContext.SaveChanges();
            }

            return criteria.User;
        }
    }
}
