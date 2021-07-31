using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class Authorization
    {
        public Guid SecurityTokenId { get; set; }
        public Guid UserId { get; set; }
        public string JsonWebToken { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Secret { get; set; }

        public virtual User User { get; set; }
    }
}
