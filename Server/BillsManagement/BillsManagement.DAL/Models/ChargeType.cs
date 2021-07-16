using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class ChargeType
    {
        public ChargeType()
        {
            Charges = new HashSet<Charge>();
        }

        public Guid ChargeTypeId { get; set; }
        public string ChargeType1 { get; set; }

        public virtual ICollection<Charge> Charges { get; set; }
    }
}
