namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System;

    public class ChargesRepository : BaseRepository, IChargesRepository
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

            var entity = this._mapper.Map<DomainModel.Charge, Charge>(charge);

            this._dbContext.Add(entity);
            this._dbContext.SaveChanges();

            return charge;
        }
    }
}
