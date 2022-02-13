using MyStock.Application.Documents.Validators;

namespace MyStock.Application.Documents
{
    public class DocumentViewModel : EntityViewModel<Document, DocumentValidator>
    {
        private DateTime date;
        private string description;
        private decimal discount;

        public DocumentViewModel(Document entity, IContext context) : base(entity, context) {
            Date = DateTime.Now.Date;
        }

        public DateTime Date { get => date; set => RaiseAndSetAndValidateIfChanged(ref date, value); }

        public string Description { get => description; set => RaiseAndSetAndValidateIfChanged(ref description, value); }

        public decimal Discount
        {
            get => discount;
            set
            {
                if (RaiseAndSetAndValidateIfChanged(ref discount, value))
                    RaisePropertyChanged(new[] { nameof(Balance) });
            }
        }

        public decimal TotalPrice => Details.Sum(d => d.TotalPrice);

        public decimal Balance => TotalPrice - Math.Min(Discount, TotalPrice);

        public ObservableCollection<DocumentDetailViewModel> Details { get; set; }

        protected override void InitializeAssociatedProperties()
        {
            Details = new ObservableCollection<DocumentDetailViewModel>()
            {
                new DocumentDetailViewModel(new DocumentDetail(), Context)
            };
            base.InitializeAssociatedProperties();
        }

        public override bool ValidateBeforeSaveChange()
        {
            return base.ValidateBeforeSaveChange();
        }

        public override void ActionBeforeSave()
        {
            base.ActionBeforeSave();
        }
    }
}
