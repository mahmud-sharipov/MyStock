using MyStock.Core.Enums;

namespace MyStock.Application.UIComunication
{
    public class Message : ReactiveObject, IMessage
    {
        public Message()
        {

        }

        public Message(string owner, SeverityLevel severity)
        {
            Guid = Guid.NewGuid();
            Owner = owner;
            Severity = severity;
        }

        public string Owner { get; }

        public Guid Guid { get; }

        public string Title { get; set; }

        public string Solution { get; set; }

        public string Detail { get; set; }

        public SeverityLevel Severity { get; set; }

        public void Remove() => MessageManager.Messages.Remove(this);

        public void Show() => MessageManager.Show(this);
    }
}
