namespace MyStock.Core;

public class NotificationManager
{
    private static readonly object padlock = new object();
    private static NotificationManager instance;
    public SnackbarMessageQueue SnackbarMessageQueue { get; init; }

    NotificationManager()
    {
        SnackbarMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(10))
        {
            DiscardDuplicates = true
        };
    }

    public static NotificationManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new NotificationManager();
                return instance;
            }
        }
    }

    public static void Info(string message)
    {
        var messageQueue = Instance.SnackbarMessageQueue;
        messageQueue.Enqueue(message, "x", p => { }, null, false, true, TimeSpan.FromSeconds(10));
    }

    public static void Error(string message)
    {
        var messageQueue = Instance.SnackbarMessageQueue;
        messageQueue.Enqueue(message, "Ok", p => { }, message, false, true, TimeSpan.FromSeconds(10));
    }
}
