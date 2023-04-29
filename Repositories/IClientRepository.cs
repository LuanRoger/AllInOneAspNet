using AllInOneAspNet.Models.ClientModels;

namespace AllInOneAspNet.Repositories;

public interface IClientRepository
{
    /// <summary>
    /// Cadastra um novo cliente
    /// </summary>
    /// <param name="client">Modelo contendo as infomações do cliente para cadastro</param>
    public Task<int> RegisterClient(ClientModel client);
    
    /// <summary>
    /// Resgata um cliente pelo ID
    /// </summary>
    /// <param name="clientId">ID do cliente</param>
    /// <returns>Retona o cliente encontrado ou <c>null</c></returns>
    public Task<ClientModel?> GetClientById(int clientId);
    
    /// <summary>
    /// Resgata todos os clientes cadastrados por um determinado usuário
    /// </summary>
    /// <param name="userId">ID do usuário</param>
    /// <returns>Lista contendo todos os clientes cadastrados pelo usuário</returns>
    public Task<IReadOnlyList<ClientModel>> GetUserRelatedClient(int userId);
    
    /// <summary>
    /// Deleta um cliente
    /// </summary>
    /// <param name="clientModel">Cliente para deletar</param>
    /// <returns>Informações do cliente deletado</returns>
    public ClientModel DeleteClient(ClientModel clientModel);
    
    /// <summary>
    /// Apply all changes made to the database
    /// </summary>
    /// <returns></returns>
    public Task FlushChanges();
}