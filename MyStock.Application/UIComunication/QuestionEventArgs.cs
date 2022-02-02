namespace MyStock.Application.Message
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
