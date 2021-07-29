namespace BillsManagement.DomainModel
{
    public class TokenValidator
    {
        public DomainModel.SecurityToken SecurityToken { get; set; }

        public DomainModel.Authentication Authentication { get; set; }

        public string Email { get; set; }
    }
}
