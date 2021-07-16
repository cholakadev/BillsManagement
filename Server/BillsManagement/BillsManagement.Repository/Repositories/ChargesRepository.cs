namespace BillsManagement.Repository.Repositories
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;

    public class ChargesRepository : IChargesRepository
    {
        private readonly BillsManagementContext _dbContext;

        public ChargesRepository(BillsManagementContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Charge GenerateCharge(GenerateChargeCriteria criteria)
        {
            Charge charge = new Charge()
            {
                ChargeId = criteria.Charge.ChargeId,
                ChargeDate = criteria.Charge.ChargeDate,
                ChargeType = criteria.Charge.ChargeType,
                DueAmount = criteria.Charge.DueAmount,
                UserId = criteria.Charge.UserId
            };

            this._dbContext.Add(charge);
            this._dbContext.SaveChanges();

            return charge;
        }
    }
}
