namespace Year2023.Exceptions;

internal class UnknownColourException : Exception
{
    public string Colour { get; init; }

    public UnknownColourException(string colour)
    {
        Colour = colour;
    }
}
