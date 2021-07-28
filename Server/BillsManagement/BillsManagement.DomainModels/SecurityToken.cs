namespace BillsManagement.DomainModel
{
    using System;

    public class SecurityToken
    {
        public Guid UserId { get; set; }

        public string SecurityToken1 { get; set; }

        public bool? IsExpired { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime? CreationDate { get; set; }

        public string Secret { get; set; }
    }
}
