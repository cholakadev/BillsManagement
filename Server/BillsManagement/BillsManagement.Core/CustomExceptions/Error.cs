namespace BillsManagement.Core.CustomExceptions
{
    using System.Text.Json.Serialization;

    public class Error
    {
        [JsonPropertyName("Message")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("StatusCode")]
        public int ErrorStatusCode { get; set; }
    }
}
