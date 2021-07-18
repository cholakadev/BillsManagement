namespace BillsManagement.DomainModel.User
{
    using System.Text.Json.Serialization;

    public class RegisterResponse
    {
        [JsonPropertyName("Registration")]
        public DomainModel.Registration Registration { get; set; }
    }
}
