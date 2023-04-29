namespace AllInOneAspNet.Exceptions;

public class InvalidRequestInfoException : Exception
{
    const string MESSAGE = "Invalid request parameters: {0}";
    
    public InvalidRequestInfoException(IEnumerable<string> invalidParameters) : 
        base(string.Format(MESSAGE, string.Join(" ", invalidParameters)))
    { /* Nothing */ }
}