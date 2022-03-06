namespace MyStock.Core.Interfaces;

public interface IMessage
{
    string Title { get; set; }
    string Solution { get; set; }
    string Detail { get; set; }
    SeverityLevel Severity { get; set; }
    public void Remove();
    public void Show();
}