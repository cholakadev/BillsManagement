using System.Text.Json.Serialization;

namespace BillsManagement.DomainModels.Charges
{
    public class GenerateChargeResponse
    {
        [JsonPropertyName("Charge")]
        public DomainModels.Charge Charge { get; set; }
    }
}
