using Newtonsoft.Json;

namespace ProductManagement.BL.Models.Product;

public class ProductIdRequest
{
    [JsonProperty("id", Required = Required.Always)]
    public long Id { get; set; }
}

