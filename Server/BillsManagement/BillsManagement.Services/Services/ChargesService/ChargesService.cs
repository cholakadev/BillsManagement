namespace BillsManagement.Services.Services.ChargesService
{
    using BillsManagement.DomainModel.Charges;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Services.ServiceContracts;
    using System;

    public partial class ChargesService : IChargesService
    {
        private readonly IChargesRepository _chargesRepository;

        public ChargesService(IChargesRepository chargesRepository)
        {
            this._chargesRepository = chargesRepository;
        }

        public GenerateChargeResponse GenerateCharge(GenerateChargeRequest request)
        {
            var charge = new DomainModel.Charge()
            {
                ChargeId = Guid.NewGuid(),
                ChargeDate = request.ChargeDate,
                DueAmount = request.DueAmount,
                ChargeTypeId = request.ChargeTypeId,
                UserId = request.UserId
            };

            var generatedCharge = this._chargesRepository.GenerateCharge(charge);
            GenerateChargeResponse response = new GenerateChargeResponse();
            response.Charge = generatedCharge;
            return response;
        }

        public GetChargesResponse GetCharges()
        {
            GetChargesResponse response = new GetChargesResponse();
            response.Charges = this._chargesRepository.GetCharges();
            return response;
        }

        public object RegisterPayment()
        {
            throw new System.NotImplementedException();
        }
    }
}
