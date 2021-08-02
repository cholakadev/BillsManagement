namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System.Linq;

    public class AuthorizationRepository : BaseRepository<DomainModel.User>, IAuthorizationRepository
    {
        public AuthorizationRepository(BillsManagementContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public void SaveAuthorization(DomainModel.Authorization authorization)
        {
            Authorization mappedAuthorization = this._mapper.Map<DomainModel.Authorization, Authorization>(authorization);

            this._dbContext.Authorizations.Add(mappedAuthorization);
            this._dbContext.SaveChanges();
        }

        public void UpdateToken(DomainModel.Authorization authorization)
        {
            var oldAuthorization = this._dbContext.Authorizations
                .FirstOrDefault(authorization => authorization.IsExpired == false && authorization.UserId == authorization.UserId);

            if (oldAuthorization != null)
            {
                oldAuthorization.IsExpired = true;
            }

            var mappedAuthorization = this._mapper.Map<DomainModel.Authorization, Authorization>(authorization);

            this._dbContext.Add(mappedAuthorization);
            this._dbContext.SaveChanges();
        }
    }
}
