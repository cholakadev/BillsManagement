namespace BillsManagement.Services.Services
{
    using BillsManagement.Repository.RepositoryContracts;

    public class BaseService
    {
        internal readonly IUserRepository _userRepository; // BaseRepository
        internal readonly IAuthenticationRepository _authenticationRepository;

        public BaseService(IUserRepository userRepository, IAuthenticationRepository authenticationRepository)
        {
            this._userRepository = userRepository;
            this._authenticationRepository = authenticationRepository;
        }
    }
}
