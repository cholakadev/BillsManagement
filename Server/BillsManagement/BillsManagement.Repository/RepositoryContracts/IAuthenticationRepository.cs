using BillsManagement.DAL.Models;

namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IAuthenticationRepository : IBaseRepository<Authentication>
    {
        void SaveToken(DomainModel.SecurityToken securityToken);

        void UpdateToken(DomainModel.SecurityToken securityToken);
    }
}
