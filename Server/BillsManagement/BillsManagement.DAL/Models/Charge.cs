using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class Charge
    {
        public Guid ChargeId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ChargeType { get; set; }
        public decimal? DueAmount { get; set; }
        public DateTime ChargeDate { get; set; }

        public virtual ChargeType ChargeTypeNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
