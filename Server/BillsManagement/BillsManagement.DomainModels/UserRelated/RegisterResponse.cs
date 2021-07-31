namespace BillsManagement.DomainModel
{
    using System.Text.Json.Serialization;

    public class RegisterResponse : BaseResponse
    {
        [JsonPropertyName("Registration")]
        public DomainModel.Registration Registration { get; set; }
    }
}
