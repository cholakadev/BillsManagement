namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System.Linq;

    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        public AuthenticationRepository(BillsManagementContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public void SaveToken(DomainModel.SecurityToken securityToken)
        {
            SecurityToken mappedSecurityToken = this._mapper.Map<DomainModel.SecurityToken, SecurityToken>(securityToken);

            this._dbContext.SecurityTokens.Add(mappedSecurityToken);
            this._dbContext.SaveChanges();
        }

        public void UpdateToken(DomainModel.SecurityToken securityToken)
        {
            var oldSecurityToken = this._dbContext.SecurityTokens
                .FirstOrDefault(token => token.IsExpired == false && token.UserId == securityToken.UserId);

            oldSecurityToken.IsExpired = true;

            var mappedCurrentSecurityToken = this._mapper.Map<DomainModel.SecurityToken, SecurityToken>(securityToken);

            this._dbContext.Add(mappedCurrentSecurityToken);
            this._dbContext.SaveChanges();
        }
    }
}
