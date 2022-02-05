namespace MyStock.Application.UIComunication
{
    public class QuestionEventArgs : EventArgs
    {
        public Question Question { get; set; }

        public QuestionEventArgs(Question question)
        {
            Question = question;
        }
    }
}
