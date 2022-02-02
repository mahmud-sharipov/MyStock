namespace MyStock.Application.Message
{

    public static class MessageManager
    {
        public static Message CurrentMessage;

        public static ObservableCollection<Message> Messages => new ObservableCollection<Message>();

        public delegate void MessageEventHendler(MessageEventArgs e);

        public static event MessageEventHendler ShowMessage;

        public static void Show(Message message)
        {
            CurrentMessage = message;
            Messages.Add(message);
            ShowMessage?.Invoke(new MessageEventArgs(message));
        }
    }
}
