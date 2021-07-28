using BillsManagement.DomainModel.Charges;
using BillsManagement.Services.ServiceContracts;
using System;

namespace BillsManagement.Tests.ChargesControllerTests
{
    public class ChargesServiceFake : IChargesService
    {
        ChargesRepositoryFake _repository;

        public ChargesServiceFake()
        {
            this._repository = new ChargesRepositoryFake();
        }

        public GenerateChargeResponse GenerateCharge(GenerateChargeRequest request)
        {
            throw new NotImplementedException();
        }

        public GetChargesResponse GetCharges()
        {
            GetChargesResponse response = new GetChargesResponse();
            response.Charges = this._repository.GetCharges();
            return response;
        }

        public object RegisterPayment()
        {
            throw new NotImplementedException();
        }
    }
}
