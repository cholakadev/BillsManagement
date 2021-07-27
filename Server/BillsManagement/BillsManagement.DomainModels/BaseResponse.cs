namespace BillsManagement.DomainModel
{
    using System.Net;
    using System.Text.Json.Serialization;

    public class BaseResponse
    {
        [JsonPropertyName("StatusCode")]
        public HttpStatusCode StatusCode { get; set; }
    }
}
