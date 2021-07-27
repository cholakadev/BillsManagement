namespace BillsManagement.DomainModel.Charges
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class GetChargesResponse : BaseResponse
    {
        [JsonPropertyName("Charges")]
        public List<DomainModel.Charge> Charges { get; set; }
    }
}
