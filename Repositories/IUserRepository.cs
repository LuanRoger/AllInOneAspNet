using AllInOneAspNet.Models.UserModels;

namespace AllInOneAspNet.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Cadastra um novo usuário
    /// </summary>
    /// <param name="user">Usuário para cadastrar</param>
    /// <returns>Retonar o ususário cadastrado</returns>
    public Task<UserModel> RegisterUser(UserModel user);

    /// <summary>
    /// Resgatar um usuário pelo ID
    /// </summary>
    /// <param name="userId">ID do usuário</param>
    /// <returns>Retorna o usuário que possui o ID indicado ou <c>null</c></returns>
    public Task<UserModel?> GetUserById(int userId);
    
    /// <summary>
    /// Resgatar um usuário pelo <c>username</c>
    /// </summary>
    /// <param name="username">Nome do usuario</param>
    /// <returns>Retorna um usuario que possua o nome de usuario especificado.</returns>
    public Task<UserModel?> GetUserByUsername(string username);
    
    /// <summary>
    /// Apply all changes made to the database
    /// </summary>
    public Task FlushChanges();
}