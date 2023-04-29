namespace AllInOneAspNet.Exceptions;

public class UserNotFoundException : Exception
{
    private const string MESSAGE = "User with Username[{0}] was not found";
    
    public UserNotFoundException(string username) : 
        base(string.Format(MESSAGE, username))
    { /* Nothing */ }
}