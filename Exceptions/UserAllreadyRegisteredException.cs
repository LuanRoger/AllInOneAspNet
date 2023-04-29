namespace AllInOneAspNet.Exceptions;

public class UserAllreadyRegisteredException : Exception
{
    private const string MESSAGE = "User with username {0} allready registered";
    
    public UserAllreadyRegisteredException(string username) : 
        base(string.Format(MESSAGE, username))
    { /* Nothing */ }
}