namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using System.Collections.Generic;

    public interface IChargesRepository : IBaseRepository<Charge>
    {
        DomainModel.Charge GenerateCharge(DomainModel.Charge charge);

        List<DomainModel.Charge> GetCharges();
    }
}
