namespace BillsManagement.Services.ServiceContracts
{
    using System;

    public interface IBillsService
    {
        Object RegisterPayment();

        Object GenerateCharge();
    }
}
