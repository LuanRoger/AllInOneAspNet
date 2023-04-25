using AllInOneAspNet.Models.UserModels;

namespace AllInOneAspNet.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Cadastra um novo usuário
    /// </summary>
    /// <param name="user">Usuário para cadastrar</param>
    public Task RegisterUser(UserModel user);

    /// <summary>
    /// Resgatar um usuário pelo ID
    /// </summary>
    /// <param name="userId">ID do usuário</param>
    /// <returns>Retorna o usuário que possui o ID indicado ou <c>null</c></returns>
    public Task<UserModel?> GetUserById(int userId);
    
    /// <summary>
    /// Resgatar um usuário pelo <c>username</c> e <c>password</c>
    /// </summary>
    /// <param name="username">Nome do usuario</param>
    /// <param name="password">Senha do usuario</param>
    /// <returns></returns>
    public Task<UserModel?> GetUserByUsernamePassword(string username, string password);
}