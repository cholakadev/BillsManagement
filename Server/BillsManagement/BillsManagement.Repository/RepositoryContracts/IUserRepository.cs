namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.Models;

    public interface IUserRepository
    {
        User Register(RegisterCriteria criteria);

        bool IsExistingUser(string email);

        User GetUserInformation(Criteria getUserInformationSearchCriteria);
    }
}
