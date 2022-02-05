namespace MyStock.Application.UIComunication
{
    public class QuestionChoice : IQuestionChoice
    {
        public QuestionChoice(IQuestion question, string title, Action<IQuestionChoice> action)
        {
            Title = title;
            Action = action;
            Question = question;
        }
        public string Title { get; }
        public Action<IQuestionChoice> Action { get; }
        public IQuestion Question { get; }
    }
}
