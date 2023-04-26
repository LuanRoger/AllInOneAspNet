using AllInOneAspNet.Models.UserModels;

namespace AllInOneAspNet.Controllers;

public interface IUserController
{
    /// <summary>
    /// Cadastra um novo usuário
    /// </summary>
    /// <param name="signinRequest">Requisição de cadastro do usuário</param>
    /// <returns>Retorna o ID do novo usuário</returns>
    public Task<int> SigninUser(UserSigninRequestModel signinRequest);
    
    /// <summary>
    /// Autentica um usuário
    /// </summary>
    /// <param name="loginRequest">Requisição de login do usuário</param>
    /// <returns>Retorna um novo JWT para o usuário</returns>
    public Task<string> LoginUser(UserLoginRequestModel loginRequest);
}