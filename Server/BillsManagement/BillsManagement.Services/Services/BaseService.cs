namespace BillsManagement.Services.Services
{
    using BillsManagement.Repository.RepositoryContracts;
    using System;

    public class BaseService
    {
        internal readonly IUserRepository _userRepository; // BaseRepository

        public BaseService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        void ValidateJwtToken(Guid userId)
        {
            DomainModel.SecurityToken token = this._userRepository.GetSecurityTokenByUserId(userId);

            if (token.ExpirationDate <= DateTime.Now)
            {
                throw new Exception("Session expired.");
            }
        }
    }
}
