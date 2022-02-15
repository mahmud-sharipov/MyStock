using MyStock.Application.Products;

namespace MyStock.Application.Documents
{
    public class DocumentViewModel<TEntity, TValidator, TPage> : EntityPageViewModel<TEntity, TValidator, TPage>
        where TEntity : Document
        where TValidator : class, IValidator
        where TPage : class, IEntityPage
    {
        private DateTime date;
        private string description;
        private decimal discount;
        private decimal paidAmount;
        private DocumentDetailViewModel newLine;
        private DocumentDetailViewModel selectedDetail;
        private bool processed;

        public DocumentViewModel(TEntity entity, IContext context) : base(entity, context)
        {
            Date = DateTime.Now.Date;
        }

        public bool Processed { get => processed; set => RaiseAndSetIfChanged(ref processed, value); }
        public DocumentDetailViewModel SelectedDetail { get => selectedDetail; set => RaiseAndSetIfChanged(ref selectedDetail, value); }
        public DocumentDetailViewModel NewLine { get => newLine; set => RaiseAndSetIfChanged(ref newLine, value); }
        public DateTime Date { get => date; set => RaiseAndSetAndValidateIfChanged(ref date, value); }
        public string Description { get => description; set => RaiseAndSetAndValidateIfChanged(ref description, value); }
        public decimal Discount { get => discount; set => RaiseAndSetAndValidateIfChanged(ref discount, value, nameof(Discount), nameof(Balance)); }
        public decimal PaidAmount { get => paidAmount; set => RaiseAndSetAndValidateIfChanged(ref paidAmount, value, nameof(PaidAmount), nameof(Balance)); }
        public decimal TotalPrice => Details.Sum(d => d.UnitPrice * d.Quantity);
        public decimal Balance => TotalPrice - Math.Min(Discount, TotalPrice);
        public ObservableCollection<DocumentDetailViewModel> Details { get; set; }
        public string DialogIdentifier => "DocumentPageDialogHost";

        public ICommand AcceptPayment { get; protected set; }
        public ICommand Process { get; protected set; }
        public ICommand DeleteDetail { get; protected set; }
        public ICommand AddDetail { get; protected set; }
        public ICommand IncremetQuantity { get; protected set; }
        public ICommand DecrementQuantity { get; protected set; }

        protected override void InitializeAssociatedProperties()
        {
            Details = new ObservableCollection<DocumentDetailViewModel>();
            foreach (var detail in Entity.Details)
            {
                var vm = new DocumentDetailViewModel(detail, Context);
                vm.PropertyChanged += Details_PropertyChanged;
                Details.Add(vm);
            }
            NewLine = new DocumentDetailViewModel(new DocumentDetail() { Document = Entity, Quantity = 1 }, Context);
            base.InitializeAssociatedProperties();
        }

        private void Details_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DocumentDetailViewModel.TotalPrice):
                    RaisePropertyChanged(new[] { nameof(TotalPrice), nameof(Balance) });
                    break;
                default:
                    break;
            }

            if (sender is DocumentDetailViewModel vm && (vm.Quantity == 0 || vm.Product == null))
            {
                Context.Delete(vm.Entity);
                Details.Remove(vm);
            }
        }

        public override bool ValidateBeforeSaveChange()
        {
            return base.ValidateBeforeSaveChange() && Details.All(d => d.ValidateBeforeSaveChange());
        }

        public override void ActionBeforeSave()
        {
            foreach (var detail in Details)
                detail.ActionBeforeSave();
            base.ActionBeforeSave();
        }

        protected override void BuildCommands()
        {
            base.BuildCommands();
            IncremetQuantity = ReactiveCommand.Create<DocumentDetailViewModel>((detail) =>
            {
                if (detail == null) return;
                SelectedDetail.Quantity++;
            }, outputScheduler: Scheduler.CurrentThread);

            DecrementQuantity = ReactiveCommand.Create<DocumentDetailViewModel>((detail) =>
            {
                if (detail == null) return;
                detail.Quantity--;
                if (detail.Quantity == 0)
                {
                    Context.Delete(detail.Entity);
                    Details.Remove(SelectedDetail);
                    SelectedDetail = null;
                }
            }, outputScheduler: Scheduler.CurrentThread);

            DeleteDetail = ReactiveCommand.Create<DocumentDetailViewModel>((detail) =>
            {
                if (detail == null) return;
                detail.PropertyChanged -= Details_PropertyChanged;
                Context.Delete(detail.Entity);
                Details.Remove(detail);
                SelectedDetail = null;
                RaisePropertyChanged(new[] { nameof(TotalPrice), nameof(Balance) });
            }, outputScheduler: Scheduler.CurrentThread);

            AddDetail = ReactiveCommand.Create(() =>
            {
                NewLine.Mapper.Map(NewLine, NewLine.Entity);
                var existDetail = Details.FirstOrDefault(d => d.Product == NewLine.Product);
                if (existDetail != null)
                    existDetail.Quantity += NewLine.Quantity;
                else
                {
                    NewLine.PropertyChanged += Details_PropertyChanged;
                    Context.AddToContext(NewLine.Entity);
                    Details.Add(NewLine);
                }
                NewLine = new DocumentDetailViewModel(new DocumentDetail() { Document = Entity, Quantity = 1 }, Context);
                RaisePropertyChanged(new[] { nameof(TotalPrice), nameof(Balance) });
            }, this.WhenAny(v => v.NewLine.IsValid, v => v.Value), outputScheduler: Scheduler.CurrentThread);
        }
    }
}
