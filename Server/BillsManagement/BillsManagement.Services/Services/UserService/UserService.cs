namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Services.ServiceContracts;
    using System;

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            this._repository = repository;
        }

        public User Register(User user, string password)
        {
            if (this._repository.IsExistingUser(user.Email))
            {
                throw new Exception("User already exists.");
            }

            var criteria = new RegisterCriteria();
            criteria.User = user;
            //criteria.Password = PasswordCipher.Encode(password);

            var response = this._repository.Register(criteria);

            return response;
        }
    }
}
