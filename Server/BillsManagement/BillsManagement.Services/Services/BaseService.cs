namespace BillsManagement.Services.Services
{
    using BillsManagement.Repository.RepositoryContracts;
    using System;

    public class BaseService
    {
        internal readonly IUserRepository _userReopsitory; // BaseRepository

        public BaseService(IUserRepository userRepository)
        {
            this._userReopsitory = userRepository;
        }

        void ValidateJwtToken(Guid userId)
        {
            DomainModel.SecurityToken token = this._userReopsitory.GetSecurityTokenByUserId(userId);

            if (token.ExpirationDate <= DateTime.Now)
            {
                throw new Exception("Session expired.");
            }
        }
    }
}
