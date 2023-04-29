namespace AllInOneAspNet.Exceptions;

public class WrongUserPasswordException : Exception
{
    private const string MESSAGE = "Wrong password for user with Username[{0}]";
    
    public WrongUserPasswordException(string username) : 
        base(string.Format(MESSAGE, username))
    { /* Nothing */ }
}