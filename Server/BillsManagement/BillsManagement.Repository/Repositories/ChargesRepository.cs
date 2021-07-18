namespace BillsManagement.Repository.Repositories
{
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System;

    public class ChargesRepository : BaseRepository, IChargesRepository
    {
        public ChargesRepository(BillsManagementContext dbContext)
            : base(dbContext)
        {

        }

        public Charge GenerateCharge(GenerateChargeCriteria criteria)
        {
            bool isExisting = this.CheckIfUserExistsById(criteria.Charge.UserId);

            if (!isExisting)
            {
                throw new Exception("Can't generate a charge for not existing user.");
            }

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
