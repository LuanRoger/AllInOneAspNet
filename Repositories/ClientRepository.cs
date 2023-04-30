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
    
    public async Task<ClientModel> RegisterClient(ClientModel client) =>
        (await dbContext.client.AddAsync(client)).Entity;

    public async Task<ClientModel?> GetClientById(int clientId) =>
        await dbContext.client.FindAsync(clientId);

    public async Task<IReadOnlyList<ClientModel>> GetUserRelatedClient(int userId) =>
        await dbContext.client
            .Where(client => client.createdBy.id == userId)
            .OrderBy(client => client.id)
            .ToListAsync();

    public ClientModel DeleteClient(ClientModel clientModel) =>
        dbContext.client.Remove(clientModel).Entity;
    
    public async Task FlushChanges() =>
        await dbContext.SaveChangesAsync();
}