namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;

    public interface IUserRepository
    {
        DAL.Models.User Register(RegisterCriteria criteria);

        bool IsExistingUser(string email);

        Authentication GetUserEncryptedPasswordByEmail(string email);
    }
}
