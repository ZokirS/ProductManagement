using Newtonsoft.Json;

namespace ProductManagement.BL.Models.User;

public class AuthenticateRequest : UserLoginRequest
{
    [JsonProperty("password", Required = Required.Always)]
    public string Password { get; set; }
}

