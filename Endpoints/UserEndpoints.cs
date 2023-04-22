namespace AllInOneAspNet.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("signin", (HttpContext context) => 
            Results.Created("user/signin", null));
        group.MapPost("login", (HttpContext context) => 
            Results.Ok("Logar usuario"));

        return group;
    }
}