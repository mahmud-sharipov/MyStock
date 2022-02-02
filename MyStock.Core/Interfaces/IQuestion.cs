namespace MyStock.Core.Interfaces;

public interface IQuestion
{
    string Title { get; }
    string Solution { get; }
    string Detail { get; }
    SeverityLevel Severity { get; }
    ICollection<IQuestionChoice> Choices { get; }
    IQuestionChoice SelectedChoice { get; set; }
    void RemoveMessage();
}
