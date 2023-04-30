using AllInOneAspNet.Controllers;
using AllInOneAspNet.Exceptions;
using AllInOneAspNet.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace AllInOneAspNet.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("signin", 
             async ([FromServices] UserController controller, 
                 [FromBody] UserSigninRequestModel signinRequest) =>
        {
            string newUserJwt;
            try
            {
                newUserJwt = await controller.SigninUser(signinRequest);
            }
            catch(InvalidRequestInfoException e)
            {
                return Results.BadRequest(e.Message);
            }
            catch(UserAllreadyRegisteredException e)
            {
                return Results.Conflict(e.Message);
            }
            
            return Results.Created("user/signin", newUserJwt);
        });
        group.MapPost("login", 
             async ([FromServices] UserController controller,
             [FromBody] UserLoginRequestModel loginRequest) =>
        {
            string userJwt;
            try
            {
                userJwt = await controller.LoginUser(loginRequest);
            }
            catch(Exception e) when (e is InvalidRequestInfoException or 
                                         UserNotFoundException or
                                         WrongUserPasswordException)
            {
                return Results.BadRequest(e.Message);
            }
            
            return Results.Ok(userJwt);
        });

        return group;
    }
}