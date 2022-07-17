namespace Rosetta;

public sealed class RosettaException : Exception
{
    public RosettaException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
