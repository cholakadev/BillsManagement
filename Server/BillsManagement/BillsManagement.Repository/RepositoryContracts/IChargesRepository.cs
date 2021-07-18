namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IChargesRepository
    {
        DomainModel.Charge GenerateCharge(DomainModel.Charge charge);
    }
}
