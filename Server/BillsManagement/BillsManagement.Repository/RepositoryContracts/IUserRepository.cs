namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;

    public interface IUserRepository
    {
        User Register(RegisterCriteria criteria);

        bool IsExistingUser(string email);

        User GetUserInformation(Criteria getUserInformationSearchCriteria);
    }
}
