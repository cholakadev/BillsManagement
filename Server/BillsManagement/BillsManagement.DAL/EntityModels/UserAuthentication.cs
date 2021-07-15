using System;

namespace BillsManagement.DAL.EntityModels
{
    public class UserAuthentication
    {
        public Guid UserId { get; set; }

        public string EncrypedPassword { get; set; }

        public string Email { get; set; }
    }
}
