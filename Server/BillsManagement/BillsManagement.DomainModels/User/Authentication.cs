using System;

namespace BillsManagement.DomainModel
{
    public class Authentication
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }
}
