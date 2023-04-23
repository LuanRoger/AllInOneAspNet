using AllInOneAspNet.Models.UserModels;

namespace AllInOneAspNet.Models.ClientModels;

public class ClientReadModel
{
    public string username { get; init; }
    public UserReadModel createdBy { get; init; }
    
    /// <summary>
    /// Converte um <c>ClientModel</c> em um <c>ClientReadModel</c> 
    /// </summary>
    /// <param name="clientModel"><c>ClientModel</c> que será convertido</param>
    /// <returns>Um novo <c>ClientReadModel</c> referente ao <c>ClientModel</c></returns>
    public static ClientReadModel FromClientModel(ClientModel clientModel) => new()
    {
        username = clientModel.username,
        createdBy = UserReadModel.FromUserModel(clientModel.createdBy)
    };
}