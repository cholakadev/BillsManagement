namespace BillsManagement.Repository.Repositories
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.EntityModels;
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

        public UserAuthentication GetUserEncryptedPasswordByEmail(string email)
        {
            var databaseUser = this._dbContext.Users.FirstOrDefault(x => x.Email == email);

            if (databaseUser == null)
            {
                throw new Exception("User does not exist");
            }

            var databaseAuth = this._dbContext.Authentications.FirstOrDefault(x => x.UserId == databaseUser.UserId);

            var userAuth = new UserAuthentication()
            {
                UserId = databaseUser.UserId,
                EncrypedPassword = databaseAuth.Password,
                Email = databaseUser.Email
            };

            return userAuth;
        }

        public bool IsExistingUser(string email)
        {
            DAL.Models.User user = this._dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public DAL.Models.User Register(RegisterCriteria criteria)
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
