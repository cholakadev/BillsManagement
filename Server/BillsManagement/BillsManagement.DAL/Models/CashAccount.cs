using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class CashAccount
    {
        public Guid CashAccountId { get; set; }
        public Guid? UserId { get; set; }
        public decimal Balance { get; set; }

        public virtual User User { get; set; }
    }
}
