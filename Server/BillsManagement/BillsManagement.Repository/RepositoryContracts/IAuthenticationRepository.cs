namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IAuthenticationRepository
    {
        void SaveToken(DomainModel.SecurityToken securityToken);

        void UpdateToken(DomainModel.SecurityToken securityToken);
    }
}
