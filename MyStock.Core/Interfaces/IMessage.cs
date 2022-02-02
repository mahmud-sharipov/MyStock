namespace MyStock.Core.Interfaces;

public interface IMessage
{
    string Title { get; }
    string Solution { get; }
    string Detail { get; }
    SeverityLevel Severity { get; }
    void RemoveMessage();
}