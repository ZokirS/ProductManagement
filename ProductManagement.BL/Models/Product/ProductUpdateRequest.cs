using Newtonsoft.Json;

namespace ProductManagement.BL.Models.Product;

    public class ProductUpdateRequest: ProductAddRequest
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }
    }

