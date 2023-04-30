using AllInOneAspNet.Exceptions;
using AllInOneAspNet.Models.UserModels;
using AllInOneAspNet.Repositories;
using AllInOneAspNet.Services.Jwt;
using FluentValidation;
using FluentValidation.Results;
using ILogger = Serilog.ILogger;

namespace AllInOneAspNet.Controllers;

public class UserController : IUserController
{
    private UserRepository repository { get; }
    private JwtService jwtService { get; }
    private ILogger logger { get; }
    private IValidator<UserLoginRequestModel> loginValidator { get; }
    private IValidator<UserSigninRequestModel> signinValidator { get; }
    
    public UserController(
        UserRepository repository, JwtService jwtService, ILogger logger, 
        IValidator<UserLoginRequestModel> loginValidator, 
        IValidator<UserSigninRequestModel> signinValidator)
    {
        this.repository = repository;
        this.jwtService = jwtService;
        this.logger = logger;
        this.loginValidator = loginValidator;
        this.signinValidator = signinValidator;
    }
    
    public async Task<string> SigninUser(UserSigninRequestModel signinRequest)
    {
        #region Validation
        logger.Information("Validating user signin request");
        
        ValidationResult validationResult = await signinValidator.ValidateAsync(signinRequest);
         if(!validationResult.IsValid)
         {
             InvalidRequestInfoException invalidInfoException = new(validationResult.Errors
                 .Select(e => e.ErrorMessage));
             logger.Error("Invalid user signin request: {InvalidInfoException}", invalidInfoException.Message);
             throw invalidInfoException;
         }
         UserModel? allreadyRegisteredUser = await repository.GetUserByUsername(signinRequest.username);
         if(allreadyRegisteredUser is not null)
         {
             UserAllreadyRegisteredException allreadyRegisteredException = new(signinRequest.username);
                logger.Error("User Username[{Username}] allready registered: {AllreadyRegisteredException}", 
                    signinRequest.username, allreadyRegisteredException.Message);
                throw allreadyRegisteredException;
         }

         logger.Information("User signin request is valid");
         #endregion
         
         #region Register user
         UserModel user = new()
         {
             username = signinRequest.username,
             email = signinRequest.email,
             password = signinRequest.password
         };
         UserModel registeredUser = await repository.RegisterUser(user);
         await repository.FlushChanges();
         
         logger.Information("User Username[{Username}] registered", user.username);
         #endregion

         #region Create JWT
         logger.Information("Creating JWT for user Username[{Username}]", user.username);
         string newUserJwt = jwtService.GenerateToken(registeredUser.id);
         #endregion
         
         return newUserJwt;
    }

    public async Task<string> LoginUser(UserLoginRequestModel loginRequest)
    {
        #region Validation
        logger.Information("Validating user login request");
        ValidationResult validationResult = await loginValidator.ValidateAsync(loginRequest);
        if(!validationResult.IsValid)
        {
            InvalidRequestInfoException invalidInfoException = new(validationResult.Errors
                .Select(e => e.ErrorMessage));
            logger.Error("Invalid user login request: {InvalidInfoException}", invalidInfoException.Message);
            throw invalidInfoException;
        }
        #endregion
        
        #region Login user
        logger.Information("Logging in user Username[{Username}]", loginRequest.username);
        UserModel? user = await repository.GetUserByUsername(loginRequest.username);
        if(user is null)
        {
            UserNotFoundException userNotFoundException = new(loginRequest.username);
            logger.Error("User Username[{Username}] not found: {UserNotFoundException}", 
                loginRequest.username, userNotFoundException.Message);
            throw userNotFoundException;
        }
        if(user.password != loginRequest.password)
        {
            WrongUserPasswordException wrongUserPasswordException = new(loginRequest.username);
            logger.Error("Invalid password for user Username[{Username}]: {InvalidPasswordException}", 
                loginRequest.username, wrongUserPasswordException.Message);
            throw wrongUserPasswordException;
        }
        logger.Information("User logged in Username[{Username}]", loginRequest.username);
        #endregion
        
        #region Create JWT
        logger.Information("Creating JWT for user Username[{Username}]", loginRequest.username);
        string userJwt = jwtService.GenerateToken(user.id);
        #endregion
        
        return userJwt;
    }
}