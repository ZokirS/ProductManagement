using Newtonsoft.Json;

namespace ProductManagement.BL.Models.Product;

public class ProductTitleRequest
{
    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; set; }
}

