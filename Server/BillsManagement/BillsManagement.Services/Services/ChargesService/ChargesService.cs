namespace BillsManagement.Services.Services.ChargesService
{
    using BillsManagement.DomainModel.Charges;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Services.ServiceContracts;
    using System;

    public partial class ChargesService : IChargesService
    {
        private readonly IChargesRepository _repository;

        public ChargesService(IChargesRepository repository)
        {
            this._repository = repository;
        }

        public GenerateChargeResponse GenerateCharge(GenerateChargeRequest request)
        {
            //var criteria = new GenerateChargeCriteria()
            //{
            //    Charge = new Charge()
            //    {
            //        ChargeId = Guid.NewGuid(),
            //        ChargeDate = request.ChargeDate,
            //        ChargeType = request.ChargeTypeId,
            //        DueAmount = request.DueAmount,
            //        UserId = request.UserId
            //    }
            //};

            var charge = new DomainModel.Charge()
            {
                ChargeId = Guid.NewGuid(),
                ChargeDate = request.ChargeDate,
                DueAmount = request.DueAmount,
                ChargeType = request.ChargeTypeId,
                UserId = request.UserId
            };

            var generatedCharge = this._repository.GenerateCharge(charge);
            GenerateChargeResponse response = new GenerateChargeResponse();
            response.Charge = generatedCharge;
            return response;
        }

        public object RegisterPayment()
        {
            throw new System.NotImplementedException();
        }
    }
}
