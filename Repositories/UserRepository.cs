using AllInOneAspNet.Models.UserModels;
using AllInOneAspNet.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AllInOneAspNet.Repositories;

public class UserRepository : IUserRepository
{
    private DatabaseContext dbContext { get; }
    
    public UserRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task RegisterUser(UserModel user) => 
        await dbContext.user.AddAsync(user);

    public async Task<UserModel?> GetUserById(int userId) => 
        await dbContext.user.FindAsync(userId);

    public async Task<UserModel?> GetUserByUsernamePassword(string username, string password) =>
        await dbContext.user
            .FirstOrDefaultAsync(user => user.username == username && 
                                         user.password == password);
}