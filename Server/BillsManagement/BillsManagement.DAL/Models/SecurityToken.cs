using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class SecurityToken
    {
        public Guid SecurityTokenId { get; set; }
        public Guid UserId { get; set; }
        public string SecurityToken1 { get; set; }
        public bool? IsExpired { get; set; }

        public virtual User User { get; set; }
    }
}
