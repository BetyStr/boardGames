namespace BusinessLayer.Exceptions;

public class ContextNotFoundException : Exception
{
    public ContextNotFoundException(string? message) : base(message)
    {
    }
}