namespace MyStock.Core.Interfaces
{
    public class RouterNavigationEventArgs : EventArgs
    {
        public IViewable ToPage { get; }
        public IViewable FromPage { get; }

        public RouterNavigationEventArgs(IViewable from, IViewable to)
        {
            FromPage = from;
            ToPage = to;
        }
    }
}
