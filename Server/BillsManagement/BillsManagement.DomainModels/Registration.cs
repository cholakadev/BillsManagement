using System.Text.Json.Serialization;

namespace BillsManagement.DomainModels
{
    public class Registration
    {
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("MiddleName")]
        public string MiddleName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("Phone")]
        public string Phone { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }
    }
}
