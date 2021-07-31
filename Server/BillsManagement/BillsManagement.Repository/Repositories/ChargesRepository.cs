namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ChargesRepository : BaseRepository<Charge>, IChargesRepository
    {
        public ChargesRepository(BillsManagementContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public DomainModel.Charge GenerateCharge(DomainModel.Charge charge)
        {
            bool isExisting = this.CheckIfUserExistsById(charge.UserId);

            if (!isExisting)
            {
                throw new Exception("Can't generate a charge for not existing user.");
            }

            CashAccount cashAccount = this.GetCashAccountByUserId(charge.UserId);
            if (cashAccount == null)
            {
                cashAccount = this.CreateCashAccountForUserByUserId(charge.UserId);
            }

            this.UpdateCashAccountBalanceAfterCharge(cashAccount, charge.DueAmount);

            var entity = this._mapper.Map<DomainModel.Charge, Charge>(charge);

            this._dbContext.Charges.Add(entity);
            this._dbContext.SaveChanges();

            return charge;
        }

        private void UpdateCashAccountBalanceAfterCharge(CashAccount cashAccount, decimal dueAmount)
        {
            cashAccount.Balance -= dueAmount;
            this._dbContext.Update(cashAccount);
            this._dbContext.SaveChanges();
        }

        private CashAccount CreateCashAccountForUserByUserId(Guid? userId)
        {
            CashAccount cashAccount = new CashAccount()
            {
                CashAccountId = Guid.NewGuid(),
                Balance = 0,
                UserId = userId
            };

            this._dbContext.CashAccounts.Add(cashAccount);
            this._dbContext.SaveChanges();

            return cashAccount;
        }

        public List<DomainModel.Charge> GetCharges()
        {
            List<Charge> charges = this._dbContext.Charges.ToList();

            List<DomainModel.Charge> mappedCharges = new List<DomainModel.Charge>();
            foreach (var charge in charges)
            {
                var mappedCharge = this._mapper.Map<Charge, DomainModel.Charge>(charge);
                mappedCharges.Add(mappedCharge);
            }

            return mappedCharges;
        }
    }
}
