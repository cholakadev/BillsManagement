namespace BillsManagement.DAL.CriteriaModels
{
    using BillsManagement.DAL.Models;

    public class RegisterCriteria : Criteria
    {
        public User User { get; set; }

        public string Password { get; set; }
    }
}
