namespace MyStock.Core.Report
{
    public class Report
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public Action Action { get; set; }
    }
}
