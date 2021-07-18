namespace BillsManagement.DomainModel.Charges
{
    using System;

    public class GenerateChargeRequest
    {
        public Guid UserId { get; set; }

        public decimal DueAmount { get; set; }

        public DateTime ChargeDate { get; set; }

        public Guid ChargeTypeId { get; set; }
    }
}
