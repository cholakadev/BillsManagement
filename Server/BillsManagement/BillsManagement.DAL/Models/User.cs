using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Authorizations = new HashSet<Authorization>();
            CashAccounts = new HashSet<CashAccount>();
            Charges = new HashSet<Charge>();
        }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Authorization> Authorizations { get; set; }
        public virtual ICollection<CashAccount> CashAccounts { get; set; }
        public virtual ICollection<Charge> Charges { get; set; }
    }
}
