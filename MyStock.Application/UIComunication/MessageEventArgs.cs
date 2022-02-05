namespace MyStock.Application.UIComunication
{
    public class MessageEventArgs : EventArgs
    {
        public Message Message { get; set; }

        public MessageEventArgs(Message message)
        {
            Message = message;
        }
    }
}
