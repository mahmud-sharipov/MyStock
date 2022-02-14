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
        private DocumentDetailViewModel newLine;
        private DocumentDetail selectedDetail;
        private bool processed;

        public DocumentViewModel(TEntity entity, IContext context) : base(entity, context)
        {
            Date = DateTime.Now.Date;
        }
        public bool Processed { get => processed; set => RaiseAndSetIfChanged(ref processed, value); }
        public DocumentDetail SelectedDetail { get => selectedDetail; set => RaiseAndSetIfChanged(ref selectedDetail, value); }
        public DocumentDetailViewModel NewLine { get => newLine; set => RaiseAndSetIfChanged(ref newLine, value); }
        public DateTime Date { get => date; set => RaiseAndSetAndValidateIfChanged(ref date, value); }
        public string Description { get => description; set => RaiseAndSetAndValidateIfChanged(ref description, value); }
        public decimal Discount { get => discount; set => RaiseAndSetAndValidateIfChanged(ref discount, value, nameof(Discount), nameof(Balance)); }
        public decimal TotalPrice => Details.Sum(d => d.UnitPrice * d.Quantity);
        public decimal Balance => TotalPrice - Math.Min(Discount, TotalPrice);
        public ObservableCollection<DocumentDetail> Details { get; set; }

        public ICommand AcceptPayment { get; protected set; }
        public ICommand Process { get; protected set; }

        public ICommand IncremetQuantity { get; protected set; }
        public ICommand DecrementQuantity { get; protected set; }
        public ICommand EnterQuantity { get; protected set; }
        public ICommand DeleteDetail { get; protected set; }

        public ICommand AddDetail { get; protected set; }

        protected override void InitializeAssociatedProperties()
        {
            Details = Entity.Details.ToObservableCollection();
            NewLine = new DocumentDetailViewModel(new DocumentDetail() { Document = Entity }, Context);
            base.InitializeAssociatedProperties();
        }

        protected override void BuildCommands()
        {
            base.BuildCommands();
            var editDetailObservable = this.WhenAny(v => v.SelectedDetail, v => v.Processed, (sd, p) => sd.Value != null && !p.Value);
            IncremetQuantity = ReactiveCommand.Create(() =>
            {
                SelectedDetail.Quantity++;
                RaisePropertyChanged(new[] { nameof(TotalPrice), nameof(Balance) });
            }, editDetailObservable, outputScheduler: Scheduler.CurrentThread);
            DecrementQuantity = ReactiveCommand.Create(() =>
            {
                SelectedDetail.Quantity--;
                RaisePropertyChanged(new[] { nameof(TotalPrice), nameof(Balance) });
            }, editDetailObservable, outputScheduler: Scheduler.CurrentThread);
            EnterQuantity = ReactiveCommand.Create(() =>
            {
                SelectedDetail.Quantity++;
                RaisePropertyChanged(new[] { nameof(TotalPrice), nameof(Balance) });
            }, editDetailObservable, outputScheduler: Scheduler.CurrentThread);
            DeleteDetail = ReactiveCommand.Create(() =>
            {
                Context.Delete(SelectedDetail);
                Details.Remove(SelectedDetail);
                SelectedDetail = null;
                RaisePropertyChanged(new[] { nameof(TotalPrice), nameof(Balance) });
            }, editDetailObservable, outputScheduler: Scheduler.CurrentThread);

            AddDetail = ReactiveCommand.Create(() =>
            {
                NewLine.Mapper.Map(NewLine, NewLine.Entity);
                Context.Delete(NewLine.Entity);
                Details.Add(NewLine.Entity);
                NewLine = new DocumentDetailViewModel(new DocumentDetail() { Document = Entity }, Context);
                RaisePropertyChanged(new[] { nameof(TotalPrice), nameof(Balance) });
            }, this.WhenAny(v => v.NewLine.IsValid, v => v.Value), outputScheduler: Scheduler.CurrentThread);
        }
    }
}
