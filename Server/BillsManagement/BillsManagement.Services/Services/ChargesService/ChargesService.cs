namespace BillsManagement.Services.Services.ChargesService
{
    using AutoMapper;
    using BillsManagement.DAL.CriteriaModels;
    using BillsManagement.DAL.Models;
    using BillsManagement.DomainModels.Charges;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Services.ServiceContracts;
    using System;

    public partial class ChargesService : IChargesService
    {
        private readonly IMapper _mapper;
        private readonly IChargesRepository _repository;

        public ChargesService(IChargesRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public GenerateChargeResponse GenerateCharge(GenerateChargeRequest request)
        {
            var criteria = new GenerateChargeCriteria()
            {
                Charge = new Charge()
                {
                    ChargeId = Guid.NewGuid(),
                    ChargeDate = request.ChargeDate,
                    ChargeType = request.ChargeTypeId,
                    DueAmount = request.DueAmount,
                    UserId = request.UserId
                }
            };

            var charge = this._repository.GenerateCharge(criteria);
            var mappedCharge = this._mapper.Map<Charge, GenerateChargeResponse>(charge);

            return mappedCharge;
        }

        public object RegisterPayment()
        {
            throw new System.NotImplementedException();
        }
    }
}
