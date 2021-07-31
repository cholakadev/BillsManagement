namespace BillsManagement.DomainModel
{
    public class TokenValidator
    {
        public DomainModel.Authorization SecurityToken { get; set; }

        public DomainModel.User User { get; set; }
    }
}
