using AllInOneAspNet.Models.ClientModels;
using AllInOneAspNet.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AllInOneAspNet.Repositories;

public class ClientRepository : IClientRepository
{
    private DatabaseContext dbContext { get; }
    
    public ClientRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task RegisterClient(ClientModel client) =>
        await dbContext.client.AddAsync(client);

    public async Task<ClientModel?> GetClientById(int clientId) =>
        await dbContext.client.FindAsync(clientId);

    public async Task<IReadOnlyList<ClientModel>> GetUserRelatedClient(int userId) =>
        await dbContext.client
            .Where(client => client.createdBy.id == userId)
            .OrderBy(client => client.id)
            .ToListAsync();

    public async Task<ClientModel> DeleteClient(ClientModel clientModel) =>
        dbContext.client.Remove(clientModel).Entity;
}