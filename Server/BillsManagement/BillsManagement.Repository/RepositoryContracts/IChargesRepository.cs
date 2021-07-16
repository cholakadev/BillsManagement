namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;

    public interface IChargesRepository
    {
        Charge GenerateCharge(GenerateChargeCriteria criteria);
    }
}
