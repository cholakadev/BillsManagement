using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Authentications = new HashSet<Authentication>();
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

        public virtual ICollection<Authentication> Authentications { get; set; }
        public virtual ICollection<Charge> Charges { get; set; }
    }
}
