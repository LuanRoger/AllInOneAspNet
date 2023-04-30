using AllInOneAspNet.Exceptions;
using AllInOneAspNet.Models.ClientModels;
using AllInOneAspNet.Models.UserModels;
using AllInOneAspNet.Repositories;
using FluentValidation;
using FluentValidation.Results;
using ILogger = Serilog.ILogger;

namespace AllInOneAspNet.Controllers;

public class ClientController : IClientController
{
    private ClientRepository repository { get; }
    private UserRepository userRepository { get; }
    private ILogger logger { get; }
    private IValidator<ClientRegisterRequestModel> clientRegisterValidator { get; }
    private IValidator<ClientUpdateRequestModel> clientUpdateValidator { get; }

    public ClientController(
        ClientRepository repository, UserRepository userRepository, 
        ILogger logger,
        IValidator<ClientRegisterRequestModel> clientRegisterValidator, 
        IValidator<ClientUpdateRequestModel> clientUpdateValidator)
    {
        this.repository = repository;
        this.userRepository = userRepository;
        this.logger = logger;
        this.clientRegisterValidator = clientRegisterValidator;
        this.clientUpdateValidator = clientUpdateValidator;
    }
    
    public async Task<int> RegisterClient(ClientRegisterRequestModel registerRequestModel, int userId)
    {
        #region Validation
        logger.Information("Validating client register request");
        ValidationResult validationResult = await clientRegisterValidator.ValidateAsync(registerRequestModel);
        if(!validationResult.IsValid)
        {
            InvalidRequestInfoException invalidInfoException = new(validationResult.Errors
                .Select(e => e.ErrorMessage));
            logger.Error("Invalid client register request: {InvalidInfoException}", invalidInfoException.Message);
            throw invalidInfoException;
        }
        UserModel? relatedUser = await userRepository.GetUserById(userId);
        if(relatedUser is null)
        {
            UserNotFoundException userNotFoundException = new(userId.ToString());
            logger.Error("User ID[{Id}] not found: {UserNotFoundException}", 
                userId, userNotFoundException.Message);
            throw userNotFoundException;
        }
        #endregion
        
        #region Register client
        logger.Information("Registering client...");
        ClientModel client = new()
        {
            username = registerRequestModel.username,
            createdBy = relatedUser
        };
        ClientModel newClient = await repository.RegisterClient(client);
        await repository.FlushChanges();
        #endregion
        
        logger.Information("Client registered successfully");
        return newClient.id;
    }

    public async Task<IReadOnlyList<ClientReadModel>> GetUserClients(int userId)
    {
        #region Validation
        logger.Information("Validating user ID[{Id}]", userId);
        UserModel? user = await userRepository.GetUserById(userId);
        if(user is null)
        {
            UserNotFoundException userNotFoundException = new(userId.ToString());
            logger.Error("User ID[{Id}] not found: {UserNotFoundException}", 
                userId, userNotFoundException.Message);
            throw userNotFoundException;
        }
        #endregion
        
        #region Get user clients
        logger.Information("Getting user ID[{Id}] clients", userId);
        var userClients = await repository.GetUserRelatedClient(userId);
        var userClientRead = 
            userClients.Select(ClientReadModel.FromClientModel).ToList();
        #endregion
        
        return userClientRead;
    }

    public async Task<int> UpdateClient(ClientUpdateRequestModel updateRequest, int clientId)
    {
        #region Validation
        logger.Information("Validating client update request");
        ValidationResult validationResult = await clientUpdateValidator.ValidateAsync(updateRequest);
        if(!validationResult.IsValid)
        {
            InvalidRequestInfoException invalidInfoException = new(validationResult.Errors
                .Select(e => e.ErrorMessage));
            logger.Error("Invalid client update request: {InvalidInfoException}", invalidInfoException.Message);
            throw invalidInfoException;
        }
        ClientModel? client = await repository.GetClientById(clientId);
        if(client is null)
        {
            ClientNotFoundException clientNotFoundException = new(clientId.ToString());
            logger.Error("Client ID[{Id}] not found: {InvalidInfoException}", 
                clientId, clientNotFoundException.Message);
            throw clientNotFoundException;
        }
        #endregion

        #region Check for updates
        bool hasUpdates = false;
        logger.Information("Checking for updates");
        if(client.username != updateRequest.username)
        {
            logger.Information("Client username changed from {OldUsername} to {NewUsername}", 
                client.username, updateRequest.username);
            client.username = updateRequest.username;
            hasUpdates = true;
        }
        #endregion

        #region Update client
        if(!hasUpdates)
        {
            logger.Warning("There are no updates to be made on ID[{Id}]", client.id);
            return client.id;
        }
        logger.Information("Updating client ID[{Id}]", client.id);
        await repository.FlushChanges();
        #endregion
        
        logger.Information("Client ID[{Id}] updated successfully", client.id);
        return client.id;
    }

    public async Task<int> DeleteClient(int clientId)
    {
        #region Validation
        logger.Information("Validating client ID[{Id}]", clientId);
        ClientModel? client = await repository.GetClientById(clientId);
        if(client is null)
        {
            ClientNotFoundException clientNotFoundException = new(clientId.ToString());
            logger.Error("Client ID[{Id}] not found: {InvalidInfoException}", 
                clientId, clientNotFoundException.Message);
            throw clientNotFoundException;
        }
        #endregion

        #region Delete client
        logger.Information("Deleting client ID[{Id}]", client.id);
        ClientModel deletedClient = repository.DeleteClient(client);
        await repository.FlushChanges();
        #endregion
        
        logger.Information("Client ID[{Id}] deleted successfully", deletedClient.id);
        return deletedClient.id;
    }
}