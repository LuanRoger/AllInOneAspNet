using AllInOneAspNet.Models.ClientModels;
using AllInOneAspNet.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace AllInOneAspNet.Repositories.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<ClientModel> client { get; set; } = null!;
    public DbSet<UserModel> user { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
}