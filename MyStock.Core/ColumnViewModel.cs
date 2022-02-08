namespace MyStock.Core
{
    public class ColumnViewModel : ReactiveObject
    {
        public ColumnViewModel(string label, string bindingPath, int order) : this(label, bindingPath, order, ColumnType.Text) { }

        public ColumnViewModel(string label, string bindingPath, int order, ColumnType type)
        {
            Label = label;
            BindingPath = bindingPath;
            AllowFilter = true;
            Order = order;
            Type = type;
        }

        public int Order { get; set; }
        public bool AllowFilter { get; set; }
        public string Label { get; }
        public string BindingPath { get; }
        public ColumnType Type { get; set; }
    }

    public enum ColumnType
    {
        Text,
        CheckBox,
        ComboBox
    }
}
