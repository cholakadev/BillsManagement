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

        public DomainModel.Authentication GetUserEncryptedPasswordByEmail(string email)
        {
            var databaseUser = this._dbContext.Users.FirstOrDefault(x => x.Email == email);

            if (databaseUser == null)
            {
                throw new Exception("User does not exist");
            }

            var auth = this._dbContext.Authentications
                .FirstOrDefault(x => x.UserId == databaseUser.UserId);

            if (auth == null)
            {
                throw new Exception("Unouthorized");
            }

            var mappedAuth = this._mapper.Map<Authentication, DomainModel.Authentication>(auth);

            return mappedAuth;
        }

        public Guid GetUserInformation(string email)
            => this._dbContext.Users
                .FirstOrDefault(x => x.Email == email).UserId;

        public bool IsExistingUser(string email)
        {
            User user = this._dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public DomainModel.Registration Register(string email, string password, out DomainModel.Settings settings)
        {
            if (email == null || email == String.Empty || password == String.Empty)
            {
                throw new Exception("Invalid request data.");
            }

            User user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = email
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

        public void UpdateToken(DomainModel.SecurityToken token)
        {
            var mappedToken = this._mapper.Map<DomainModel.SecurityToken, SecurityToken>(token);

            this._dbContext.SecurityTokens.Add(mappedToken);
            foreach (var securityToken in this._dbContext.SecurityTokens.Where(x => x.SecurityTokenId != mappedToken.SecurityTokenId))
            {
                if (securityToken.UserId == token.UserId)
                {
                    securityToken.IsExpired = true;
                }
            };

            this._dbContext.SaveChanges();
        }

        DomainModel.SecurityToken IUserRepository.GetSecurityTokenByUserId(Guid userId)
            => this.GetSecurityTokenByUserId(userId);
    }
}
