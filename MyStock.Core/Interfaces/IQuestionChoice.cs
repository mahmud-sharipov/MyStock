namespace MyStock.Core.Interfaces;

public interface IQuestionChoice
{
    string Title { get; }
    Action<IQuestionChoice> Action { get; }
    IQuestion Question { get; }
}
