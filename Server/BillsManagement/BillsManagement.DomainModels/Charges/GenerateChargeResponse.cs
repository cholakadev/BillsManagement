using System.Text.Json.Serialization;

namespace BillsManagement.DomainModel.Charges
{
    public class GenerateChargeResponse : BaseResponse
    {
        [JsonPropertyName("Charge")]
        public DomainModel.Charge Charge { get; set; }
    }
}
