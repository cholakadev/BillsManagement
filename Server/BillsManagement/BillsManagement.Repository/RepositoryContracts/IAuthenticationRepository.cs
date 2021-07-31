namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IAuthenticationRepository : IBaseRepository<DomainModel.User>
    {
        void SaveToken(DomainModel.SecurityToken securityToken);

        void UpdateToken(DomainModel.SecurityToken securityToken);
    }
}
