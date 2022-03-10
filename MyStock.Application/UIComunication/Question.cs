namespace MyStock.Application.UIComunication
{
    public class Question : ReactiveObject, IQuestion
    {
        ICollection<IQuestionChoice> _choices;

        public Question(string owner, SeverityLevel severity)
        {
            Guid = Guid.NewGuid();
            Owner = owner;
            Severity = severity;
            _choices = new Collection<IQuestionChoice>();
        }

        public string Owner { get; }

        public Guid Guid { get; }

        public string Title { get; set; }

        public string Solution { get; set; }

        public string Detail { get; set; }

        public SeverityLevel Severity { get; }

        public ICollection<IQuestionChoice> Choices => _choices;

        IQuestionChoice _selectedChoice;
        public IQuestionChoice SelectedChoice
        {
            get => _selectedChoice;
            set => _selectedChoice = value;
        }

        public void RemoveMessage()
        {
            //MessageManager.Messages.Remove(this);
        }
    }
}
