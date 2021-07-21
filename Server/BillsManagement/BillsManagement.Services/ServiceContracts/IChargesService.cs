namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DomainModel.Charges;
    using System;

    public interface IChargesService
    {
        Object RegisterPayment();

        GenerateChargeResponse GenerateCharge(GenerateChargeRequest request);

        GetChargesResponse GetCharges();
    }
}
