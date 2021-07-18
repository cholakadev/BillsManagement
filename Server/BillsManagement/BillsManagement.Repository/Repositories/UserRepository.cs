namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System;
    using System.Linq;

    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(BillsManagementContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public Authentication GetUserEncryptedPasswordByEmail(string email)
        {
            var databaseUser = this._dbContext.Users.FirstOrDefault(x => x.Email == email);

            if (databaseUser == null)
            {
                throw new Exception("User does not exist");
            }

            var auth = this._dbContext.Authentications.FirstOrDefault(x => x.UserId == databaseUser.UserId);
            return auth;
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
            User user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = criteria.Email
            };

            if (criteria != null && criteria.Password != String.Empty)
            {
                this._dbContext.Users.Add(user);

                Authentication authentication = new Authentication()
                {
                    AuthenticationId = Guid.NewGuid(),
                    Password = criteria.Password,
                    UserId = user.UserId
                };

                this._dbContext.Authentications.Add(authentication);
                this._dbContext.SaveChanges();
            }

            return user;
        }
    }
}
