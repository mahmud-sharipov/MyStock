namespace MyStock.Core
{
    public class ColumnViewModel : ReactiveObject
    {
        public ColumnViewModel(string label, string bindingPath, int order)
        {
            Label = label;
            BindingPath = bindingPath;
            AllowFilter = true;
            Order = order;
        }

        public int Order { get; set; }
        public bool AllowFilter { get; set; }
        public string Label { get; }
        public string BindingPath { get; }
    }
}
