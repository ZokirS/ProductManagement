namespace ProductManagement.BL.Models.User;

public class UserSession
{
    public string Token { get; set; }
    public DAL.Models.User User { get; set; }
}

