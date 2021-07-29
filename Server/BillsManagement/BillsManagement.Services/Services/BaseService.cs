namespace BillsManagement.Services.Services
{
    using BillsManagement.Repository.RepositoryContracts;
    using System;

    public class BaseService
    {
        internal readonly IUserRepository _userRepository; // BaseRepository
        internal readonly IAuthenticationRepository _authenticationRepository;

        public BaseService(IUserRepository userRepository, IAuthenticationRepository authenticationRepository)
        {
            this._userRepository = userRepository;
            this._authenticationRepository = authenticationRepository;
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
