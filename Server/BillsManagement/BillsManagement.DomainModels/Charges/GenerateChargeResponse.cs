namespace BillsManagement.DomainModel.Charges
{
    using System.Text.Json.Serialization;

    public class GenerateChargeResponse : BaseResponse
    {
        [JsonPropertyName("Charge")]
        public DomainModel.Charge Charge { get; set; }
    }
}
