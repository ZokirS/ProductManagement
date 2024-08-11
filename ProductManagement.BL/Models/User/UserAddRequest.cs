using Newtonsoft.Json;
using ProductManagement.DAL.Helpers;

namespace ProductManagement.BL.Models.User;

public class UserAddRequest
{
    [JsonProperty("username", Required = Required.Always)]
    public string Username { get; set; }

    [JsonProperty("password", Required = Required.Always)]
    public string Password { get; set; }

    [JsonProperty("role", Required = Required.Always)]
    public UserRole Role { get; set; }

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; set; }

    [JsonProperty("phone", Required = Required.Always)]
    public string Phone { get; set; }
}

