namespace MyStock.Application.Message
{
    public static class QuestionManager
    {
        public static Question CurrentMessage = null;

        public static ObservableCollection<Question> Questions => new ObservableCollection<Question>();

        public delegate void QuestionEventHendler(QuestionEventArgs e);

        public static event QuestionEventHendler ShowMessage;

        public static void Show(Question question)
        {
            CurrentMessage = question;
            Questions.Add(question);
            ShowMessage?.Invoke(new QuestionEventArgs(question));
        }
    }
}
