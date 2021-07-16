namespace BillsManagement.DomainModels.User
{
    using System.Text.Json.Serialization;

    public class LoginResponse
    {
        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }
}
