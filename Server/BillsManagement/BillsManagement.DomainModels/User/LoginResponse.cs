namespace BillsManagement.DomainModel
{
    using System.Text.Json.Serialization;

    public class LoginResponse : BaseResponse
    {
        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }
}
