using MyStock.Application.Documents.Validators;

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

        public decimal Quantity { get => quantity; set => RaiseAndSetAndValidateIfChanged(ref quantity, value); }
        public decimal UnitPrice { get => unitPrice; set => RaiseAndSetAndValidateIfChanged(ref unitPrice, value); }
        public decimal TotalPrice { get; }
        public string Description { get => description; set => RaiseAndSetIfChanged(ref description, value); }

        public Product Product { get => product; set => RaiseAndSetAndValidateIfChanged(ref product, value); }
        public Uom Uom { get => uom; set => RaiseAndSetAndValidateIfChanged(ref uom, value); }
        public Warehouse Warehouse { get => warehouse; set => RaiseAndSetAndValidateIfChanged(ref warehouse, value); }
    }
}
