namespace MyStock.Domain;

public class Document : EntityBase
{
    private DateTime date;
    private string description;
    private decimal discount;
    private decimal paidAmount;
    private bool processed;

    public Document()
    {
        Details = new HashSet<DocumentDetail>();
    }

    public DateTime Date { get => date; set => SetProptery(ref date, value); }
    public bool Processed { get => processed; set => SetProptery(ref processed, value); }
    public string Description { get => description; set => SetProptery(ref description, value); }
    
    public decimal Discount
    {
        get => discount;
        set
        {
            SetProptery(ref discount, value);
            RaisePropertyChanged(nameof(Total));
            RaisePropertyChanged(nameof(Balance));
        }
    }

    public decimal PaidAmount
    {
        get => paidAmount; set
        {
            SetProptery(ref paidAmount, value);
            RaisePropertyChanged(nameof(Balance));
        }
    }

    public decimal Subtotal => Details.Sum(d => d.UnitPrice * d.Quantity);
    public decimal Total => Subtotal - Discount;
    public decimal Balance => Total - PaidAmount;

    public virtual ISet<DocumentDetail> Details { get; set; }
}
