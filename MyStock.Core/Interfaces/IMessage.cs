namespace MyStock.Core.Interfaces;

public interface IMessage
{
    string Title { get; internal set; }
    string Solution { get; internal set; }
    string Detail { get; internal set; }
    SeverityLevel Severity { get; internal set; }
    public void Remove();
    public void Show();
}