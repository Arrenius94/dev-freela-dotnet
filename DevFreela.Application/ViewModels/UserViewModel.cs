namespace DevFreela.Application.ViewModels;

public class UserViewModel
{
    public UserViewModel(string fullName, string email, DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        DateOfBirth = dateOfBirth;
    }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }
}