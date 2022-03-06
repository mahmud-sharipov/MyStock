namespace MyStock.Domain;

public class Document : EntityBase
{
    private DateTime _date;
    private string _description;
    private decimal _discount;
    private decimal _paidAmount;
    private int _number;
    private bool _processed;

    public Document()
    {
        Details = new HashSet<DocumentDetail>();
    }

    public DateTime Date { get => _date; set => SetProptery(ref _date, value); }
    public bool Processed { get => _processed; set => SetProptery(ref _processed, value); }
    public string Description { get => _description; set => SetProptery(ref _description, value); }
    public int Number { get => _number; set => SetProptery(ref _number, value); }

    public decimal Discount
    {
        get => _discount;
        set
        {
            SetProptery(ref _discount, value);
            RaisePropertyChanged(nameof(Total));
            RaisePropertyChanged(nameof(Balance));
        }
    }

    public decimal PaidAmount
    {
        get => _paidAmount; set
        {
            SetProptery(ref _paidAmount, value);
            RaisePropertyChanged(nameof(Balance));
        }
    }
    public decimal Subtotal => Details.Sum(d => d.UnitPrice * d.Quantity);
    public decimal Total => Subtotal - Discount;
    public decimal Balance => Total - PaidAmount;

    public virtual ISet<DocumentDetail> Details { get; set; }

    public Guid? UserGuid { get; set; }
    public virtual User User { get; set; }
}
