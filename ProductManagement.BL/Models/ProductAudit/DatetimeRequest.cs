using Newtonsoft.Json;

namespace ProductManagement.BL.Models.ProductAudit
{
    public class DatetimeRequest
    {
        [JsonProperty("from", Required = Required.Default)]
        public DateTime? StarDate { get; set; }
        [JsonProperty("to", Required = Required.Default)]
        public DateTime? EndDate { get; set; }
    }
}