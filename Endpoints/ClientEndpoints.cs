using AllInOneAspNet.Controllers;
using AllInOneAspNet.Exceptions;
using AllInOneAspNet.Models.ClientModels;
using AllInOneAspNet.Services.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace AllInOneAspNet.Endpoints;

public static class ClientEndpoints
{
    public static RouteGroupBuilder MapClientEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", 
            async (HttpContext context, 
                [FromServices] ClientController controller) =>
        {
            int userClaimId = int.Parse(context.User.Claims
                .First(claim => claim.Type == JwtConsts.CLAIM_ID).Value);
            
            IReadOnlyList<ClientReadModel> userClients;
            try
            {
                userClients = await controller.GetUserClients(userClaimId);
            }
            catch(UserNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            
            return Results.Ok(userClients);
        });
        group.MapPost("/", 
            async (HttpContext context,
                [FromServices] ClientController controller,
                [FromBody] ClientRegisterRequestModel registerRequest) =>
        {
            int userClaimId = int.Parse(context.User.Claims
                .First(claim => claim.Type == JwtConsts.CLAIM_ID).Value);
            
            int newClientId;
            try
            {
                newClientId = await controller.RegisterClient(registerRequest, userClaimId);
            }
            catch(InvalidRequestInfoException e)
            {
                return Results.BadRequest(e.Message);
            }
            catch(UserNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            
            return Results.Created($"client/{newClientId}", newClientId);
        });
        group.MapPut("{id:int}", 
            async (HttpContext context, 
                [FromRoute] int id,
                [FromServices] ClientController controller,
                [FromBody] ClientUpdateRequestModel updateRequest) =>
        {
            int updatedClientId;
            try
            {
                updatedClientId = await controller.UpdateClient(updateRequest, id);
            }
            catch(InvalidRequestInfoException e)
            {
                return Results.BadRequest(e.Message);
            }
            catch(ClientNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            
            return Results.Ok(updatedClientId);
        });
        group.MapDelete("{id:int}", 
                async (HttpContext _,
            [FromRoute] int id,
            [FromServices] ClientController controller) =>
        {
            int deletedClientId;
            try
            {
                deletedClientId = await controller.DeleteClient(id);
            }
            catch(ClientNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            
            return Results.Ok(deletedClientId);
        });
        
        return group;
    }
}