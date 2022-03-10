namespace MyStock.Application.Documents
{
    public abstract class DocumentViewModel<TEntity, TValidator, TPage> : EntityPageViewModel<TEntity, TValidator, TPage>
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
        private ObservableCollection<DocumentDetailViewModel> details;

        public DocumentViewModel(TEntity entity, IContext context) : base(entity, context)
        {
            Date = DateTime.Now.Date;
        }

        public bool Processed { get => processed; set => RaiseAndSetIfChanged(ref processed, value, nameof(Processed), nameof(IsOpen)); }
        public bool IsOpen => !Processed;
        public DocumentDetailViewModel SelectedDetail { get => selectedDetail; set => RaiseAndSetIfChanged(ref selectedDetail, value); }
        public DocumentDetailViewModel NewLine { get => newLine; set => RaiseAndSetIfChanged(ref newLine, value); }
        public DateTime Date { get => date; set => RaiseAndSetAndValidateIfChanged(ref date, value); }
        public string Description { get => description; set => RaiseAndSetAndValidateIfChanged(ref description, value); }
        public decimal Discount { get => discount; set => RaiseAndSetAndValidateIfChanged(ref discount, value, nameof(Discount), nameof(Total), nameof(Balance)); }
        public decimal PaidAmount { get => paidAmount; set => RaiseAndSetAndValidateIfChanged(ref paidAmount, value, nameof(PaidAmount), nameof(Balance)); }
        public decimal Subtotal => Details.Sum(d => d.UnitPrice * d.Quantity);
        public decimal Total => Subtotal - Discount;
        public decimal Balance => Total - PaidAmount;
        public ObservableCollection<DocumentDetailViewModel> Details { get => details; set => RaiseAndSetAndValidateIfChanged(ref details, value); }
        public string DialogIdentifier => "DocumentPageDialogHost";

        public ICommand Process { get; protected set; }
        public ICommand Unprocess { get; protected set; }
        public ICommand DeleteDetail { get; protected set; }
        public ICommand AddDetail { get; protected set; }
        public ICommand IncremetQuantity { get; protected set; }
        public ICommand DecrementQuantity { get; protected set; }
        public ICommand Report { get; set; }

        protected override void InitializeAssociatedProperties()
        {
            var details = new ObservableCollection<DocumentDetailViewModel>();
            foreach (var detail in Entity.Details)
            {
                var vm = new DocumentDetailViewModel(detail, Context);
                vm.PropertyChanged += Details_PropertyChanged;
                details.Add(vm);
            }
            Details = details;
            NewLine = new DocumentDetailViewModel(new DocumentDetail() { Document = Entity, Quantity = 1 }, Context);
            base.InitializeAssociatedProperties();
        }

        private void Details_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DocumentDetailViewModel.TotalPrice):
                    RaisePropertyChanged();
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
                RaisePropertyChanged();
                RaiseValidation();
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
                RaisePropertyChanged();
                RaiseValidation();
            }, this.WhenAny(v => v.NewLine.IsValid, v => v.Value), outputScheduler: Scheduler.CurrentThread);

            Process = ReactiveCommand.Create(() =>
            {
                OnProcess();
                Processed = true;
                SaveChange.Execute(null);
            }, isValidObservable, outputScheduler: Scheduler.CurrentThread);

            Unprocess = ReactiveCommand.Create(() =>
            {
                OnUnprocess();
                Processed = false;
            }, this.WhenAny(v => v.Processed, v => v.Value), outputScheduler: Scheduler.CurrentThread);

        }

        void RaisePropertyChanged() =>
            RaisePropertyChanged(new[] { nameof(Total), nameof(Subtotal), nameof(Balance) });

        protected abstract void OnProcess();
        protected abstract void OnUnprocess();

        protected string GetDetailInfoForReport(DocumentDetailViewModel detailViewModel, int index)
        {
            return $"<tr><td>{++index}</td><td>{detailViewModel.Product.Code}</td><td>{detailViewModel.Product.Description}</td><td>{detailViewModel.Product.Category.Name}</td><td>{detailViewModel.Quantity:N2} {detailViewModel.Product.Uom.Code}</td><td>{detailViewModel.UnitPrice:C2}</td><td>{detailViewModel.TotalPrice:C2}</td></tr>";
        }

    }

    public enum DocumentStatus
    {
        All = 1,
        Paid = 2,
        NotPaid = 3
    }
}
