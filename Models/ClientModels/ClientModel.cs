namespace AllInOneAspNet.Models.ClientModels;

public class ClientModel
{
    public int id { get; set; }
    public string username { get; set; }
    public UserModel createdBy { get; set; }
}