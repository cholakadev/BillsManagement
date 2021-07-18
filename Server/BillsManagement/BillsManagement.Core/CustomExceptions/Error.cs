using System.Text.Json.Serialization;

namespace BillsManagement.Core.CustomExceptions
{
    public class Error
    {
        [JsonPropertyName("Message")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("StatusCode")]
        public int ErrorStatusCode { get; set; }
    }
}
