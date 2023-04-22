namespace AllInOneAspNet.Endpoints;

public static class ClientEndpoints
{
    public static RouteGroupBuilder MapClientEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", (HttpContext context) => 
            Results.Ok("Resgatar clientes"));
        group.MapPost("/", (HttpContext context) => 
            Results.Created("client/", null));
        group.MapPut("{id:int}", (HttpContext context, int id) => 
            Results.Ok($"Atualizar cliente com ID {id}"));
        group.MapDelete("{id:int}", (HttpContext context, int id) => 
            Results.Ok($"Deletar cliente com ID {id}"));
        
        return group;
    }
}