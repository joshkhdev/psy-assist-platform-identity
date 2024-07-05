namespace PsyAssistPlatformIdentity.Application.Exceptions;

public class IncorrectDataException : Exception
{
    public IncorrectDataException(string message) : base(message)
    {
    }
}