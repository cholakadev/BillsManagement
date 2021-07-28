namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
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
            User user = this._dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public DomainModel.Registration Register(DomainModel.Registration registrationRequest, string password, out DomainModel.Settings settings)
        {
            if (registrationRequest == null && password == String.Empty)
            {
                throw new Exception("Invalid request data.");
            }

            User user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = registrationRequest.Email
            };

            Authentication authentication = new Authentication()
            {
                AuthenticationId = Guid.NewGuid(),
                Password = password,
                UserId = user.UserId
            };

            var registration = this._mapper.Map<User, DomainModel.Registration>(user);

            this._dbContext.Users.Add(user);
            this._dbContext.Authentications.Add(authentication);
            this._dbContext.SaveChanges();

            settings = this.GetNotificationSettings(1);

            return registration;
        }
    }
}
