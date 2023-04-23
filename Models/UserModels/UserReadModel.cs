namespace AllInOneAspNet.Models.UserModels;

public class UserReadModel
{
    public string username { get; init; }
    public string email { get; init; }
    
    /// <summary>
    /// Converte um <c>UserModel</c> em um <c>UserReadModel</c> 
    /// </summary>
    /// <param name="userModel"><c>UserModel</c> que será convertido</param>
    /// <returns>Um novo <c>UserReadModel</c> referente ao <c>UserModel</c></returns>
    public static UserReadModel FromUserModel(UserModel userModel) => new()
    {
        username = userModel.username,
        email = userModel.email
    };
}