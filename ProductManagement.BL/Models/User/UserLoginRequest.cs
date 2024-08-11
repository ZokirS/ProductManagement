using Newtonsoft.Json;

namespace ProductManagement.BL.Models.User;

    public class UserLoginRequest
    {
    [JsonProperty("username", Required = Required.Always)]
    public string Username { get; set; }
}

