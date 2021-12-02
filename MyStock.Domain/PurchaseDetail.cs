namespace MyStock.Domain;

public class PurchaseDetail : DocumentDetail
{
    public Purchase PurchaseDocument =>Document as Purchase;
}