namespace BillsManagement.DomainModel
{
    using System;

    public class User
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public bool IsAdmin { get; set; }

        public string Password { get; set; }
    }
}
