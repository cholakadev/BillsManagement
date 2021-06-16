using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class Authentication
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }

        public virtual User User { get; set; }
    }
}
