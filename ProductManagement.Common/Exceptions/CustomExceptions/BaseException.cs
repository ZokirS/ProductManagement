namespace ProductManagement.Common.Exceptions.CustomExceptions;

public class BaseException : Exception
{
    public virtual int ErrorCode => -100;

    protected BaseException(string? message) : base(message)
    {
    }
}

