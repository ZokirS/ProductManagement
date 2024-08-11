namespace ProductManagement.Common.Exceptions.CustomExceptions;

public class UserPasswordIncorrectException(string login) : BaseException(string.Format(DefaultMessage, login))
{
    private const string DefaultMessage = "Password for user with login {0} is incorrect";

    public override int ErrorCode => -111;
}

