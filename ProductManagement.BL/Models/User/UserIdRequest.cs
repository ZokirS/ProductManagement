using Newtonsoft.Json;

namespace ProductManagement.BL.Models.User;

public class UserIdRequest
{
    [JsonProperty("id", Required = Required.Always)]
    public long Id { get; set; }
}

