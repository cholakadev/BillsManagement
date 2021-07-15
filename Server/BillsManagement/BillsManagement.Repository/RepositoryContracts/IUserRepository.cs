namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.EntityModels;

    public interface IUserRepository
    {
        DAL.Models.User Register(RegisterCriteria criteria);

        bool IsExistingUser(string email);

        UserAuthentication GetUserEncryptedPasswordByEmail(string email);
    }
}
