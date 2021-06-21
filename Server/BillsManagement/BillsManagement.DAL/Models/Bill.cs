﻿using System;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class Bill
    {
        public Guid BillId { get; set; }
        public Guid UserId { get; set; }
        public string BillName { get; set; }
        public DateTime BillDate { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public string BillStatus { get; set; }

        public virtual User User { get; set; }
    }
}
