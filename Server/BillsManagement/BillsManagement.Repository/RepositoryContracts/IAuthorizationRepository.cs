namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IAuthorizationRepository : IBaseRepository<DomainModel.User>
    {
        void SaveAuthorization(DomainModel.Authorization securityToken);

        void UpdateToken(DomainModel.Authorization securityToken);
    }
}
