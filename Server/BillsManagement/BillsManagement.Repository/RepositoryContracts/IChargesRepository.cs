namespace BillsManagement.Repository.RepositoryContracts
{
    using System.Collections.Generic;

    public interface IChargesRepository
    {
        DomainModel.Charge GenerateCharge(DomainModel.Charge charge);

        List<DomainModel.Charge> GetCharges();
    }
}
