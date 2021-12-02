namespace MyStock.Domain;

public class User : Person
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}
