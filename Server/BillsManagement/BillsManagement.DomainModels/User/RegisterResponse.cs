namespace BillsManagement.DomainModels.User
{
    using System.Text.Json.Serialization;

    public class RegisterResponse
    {
        [JsonPropertyName("Registration")]
        public DomainModels.Registration Registration { get; set; }
    }
}
