namespace BillsManagement.Core.CustomExceptions
{
    using System.Text.Json.Serialization;

    public class FaultContract
    {
        [JsonPropertyName("Error")]
        public Error Error { get; set; }
    }
}
