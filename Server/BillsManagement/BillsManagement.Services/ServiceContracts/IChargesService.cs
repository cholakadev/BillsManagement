namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DomainModels.Charges;
    using System;

    public interface IChargesService
    {
        Object RegisterPayment();

        GenerateChargeResponse GenerateCharge(GenerateChargeRequest request);
    }
}
