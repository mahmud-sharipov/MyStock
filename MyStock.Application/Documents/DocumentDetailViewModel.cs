using MyStock.Application.Documents.Validators;
using MyStock.Application.Products;

namespace MyStock.Application.Documents
{
    public class DocumentDetailViewModel : EntityViewModel<DocumentDetail, DocumentDetailValidator>
    {
        private decimal quantity;
        private decimal unitPrice;
        private string description;
        private Product product;
        private Uom uom;
        private Warehouse warehouse;

        public DocumentDetailViewModel(DocumentDetail entity, IContext context) : base(entity, context) { }

        public decimal Quantity { get => quantity; set => RaiseAndSetAndValidateIfChanged(ref quantity, value, nameof(Quantity), nameof(TotalPrice)); }
        public decimal UnitPrice { get => unitPrice; set => RaiseAndSetAndValidateIfChanged(ref unitPrice, value, nameof(UnitPrice), nameof(TotalPrice)); }
        public decimal TotalPrice => UnitPrice * Quantity;
        public string Description { get => description; set => RaiseAndSetIfChanged(ref description, value); }

        public Product Product { get => product; set => RaiseAndSetAndValidateIfChanged(ref product, value); }
        public Uom Uom { get => uom; set => RaiseAndSetAndValidateIfChanged(ref uom, value); }
        public Warehouse Warehouse { get => warehouse; set => RaiseAndSetAndValidateIfChanged(ref warehouse, value); }

        public ProductSearchViewModel ProductSearchViewModel => new ProductSearchViewModel(Context, p =>
        {
            Product = p;
            UnitPrice = product.Price;
            Uom = product.Uom;
        });
    }
}
