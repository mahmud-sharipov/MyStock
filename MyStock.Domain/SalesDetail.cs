namespace MyStock.Domain;

public class SalesDetail : DocumentDetail
{
    public Sales SalesDocument => Document as Sales;
}
