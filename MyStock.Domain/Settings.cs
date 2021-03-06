namespace MyStock.Domain;

public class Settings : EntityBase
{
    private string _companyName;
    private string _lagnuage;

    public string CompanyName { get => _companyName; set => SetProptery(ref _companyName, value); }
    public string Lagnuage { get => _lagnuage; set => SetProptery(ref _lagnuage, value); }
    public string UISettings { get; set; }
    public int NextSalesDocNumber { get; set; }
    public int NextPurchaseDocNumber { get; set; }
    public bool DefaultAnonymousCustomerOnNewSales { get; set; }
    public bool DefaultAnonymousVendorOnNewPurchase { get; set; }
}
