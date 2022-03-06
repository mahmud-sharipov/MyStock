namespace MyStock.Domain;

public class User : Person
{
    private string _login;
    private bool _isAdmin;

    public bool IsAdmin { get => _isAdmin; set => SetProptery(ref _isAdmin, value); }
    public string Login { get => _login; set => SetProptery(ref _login, value); }
    public string PasswordHash { get; set; }
}