using Newtonsoft.Json;

namespace ProductManagement.BL.Models.Product;

public class ProductAddRequest
{
    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; set; }

    [JsonProperty("quantity", Required = Required.Always)]
    public int Quantity { get; set; }

    [JsonProperty("price", Required = Required.Always)]
    public decimal Price { get; set; }

    public string? Description { get; set; }
}

