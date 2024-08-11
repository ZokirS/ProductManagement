namespace ProductManagement.Common.Exceptions.CustomExceptions;

public class UserNotFoundException : BaseException
{
    private const string LoginMessage = "User with login {0} not found";
    private const string IdMessage = "User with id {0} not found";
    
    public override int ErrorCode => -110;

    public UserNotFoundException(string login) : base(string.Format(LoginMessage, login))
    {
    }

    public UserNotFoundException(long id) : base(string.Format(IdMessage, id))
    {
    }
}