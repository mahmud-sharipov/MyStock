namespace MyStock.Core
{
    public class ColumnViewModel : ReactiveObject
    {
        public ColumnViewModel(string label, string bindingPath, int order) : this(label, bindingPath, order, ColumnType.Text) { }
        public ColumnViewModel(string label, string bindingPath, int order, string bindingStringFormat) : this(label, bindingPath, order, bindingStringFormat, ColumnType.Text) { }

        public ColumnViewModel(string label, string bindingPath, int order, ColumnType type) : this(label, bindingPath, order, string.Empty, type) { }

        public ColumnViewModel(string label, string bindingPath, int order, string bindingStringFormat, ColumnType type)
        {
            Label = label;
            BindingPath = bindingPath;
            BindingStringFormat = bindingStringFormat;
            AllowFilter = true;
            Order = order;
            Type = type;
        }

        public int Order { get; set; }
        public bool AllowFilter { get; set; }
        public string Label { get; }
        public string BindingPath { get; }
        public string BindingStringFormat { get; }
        public ColumnType Type { get; set; }
    }

    public enum ColumnType
    {
        Text,
        CheckBox,
        ComboBox
    }
}
