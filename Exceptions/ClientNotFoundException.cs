namespace AllInOneAspNet.Exceptions;

public class ClientNotFoundException : Exception
{
    private const string MESSAGE = "Client with ID[{0}] was not found";
    
    public ClientNotFoundException(string clientId) : 
        base(string.Format(MESSAGE, clientId)) 
    { /* Nothing */ }
}