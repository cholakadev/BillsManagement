namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IAuthorizationRepository : IBaseRepository<DomainModel.User>
    {
        void SaveToken(DomainModel.Authorization securityToken);

        void UpdateToken(DomainModel.Authorization securityToken);
    }
}
