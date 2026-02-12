using DevFreela.Core.Entities;

namespace DevFreela.Application.ViewModels;

public class UserViewLoginModel
{
    public UserViewLoginModel(string userName, string password)
    {
        Username = userName;
        Password = password;
    }
    public string Username { get; set; }
    public string Password { get; set; }
    
}