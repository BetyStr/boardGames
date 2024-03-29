namespace BusinessLayer.Exceptions;

public class UnauthorizedApiAccess : Exception
{
    public UnauthorizedApiAccess()
    {
    }

    public UnauthorizedApiAccess(string? message) : base(message)
    {
    }
}