namespace MyStock.Core;

public abstract class EntityPageViewModel<TEntity, TValidator, TPage> : EntityViewModel<TEntity, TValidator>, IViewable
    where TEntity : class, IEntity
    where TValidator : class, IValidator
    where TPage : class, IEntityPage
{
    private IDialogHost _dialogHost;
    private TPage _entityPage;

    protected EntityPageViewModel(TEntity entity, IContext context) : base(entity, context) { }

    public IDialogHost DialogHost =>
        _dialogHost ??= Global.Container.Resolve<IDialogHost>();

    public TPage EntityPage => _entityPage ??= Global.Container.Resolve<TPage>(new PositionalParameter(0, this));

    IEntityPage IViewable.EntityPage => EntityPage;

    public ICommand Close { get; protected set; }

    protected override void BuildCommands()
    {
        SaveChange = ReactiveCommand.Create(() =>
        {
            if (ValidateBeforeSaveChange())
            {
                ActionBeforeSave();
                Context.SaveChanges();
                DialogHost.Close(EntityPage, true);
            }
        }, isValidObservable, outputScheduler: Scheduler.CurrentThread);

        Close = ReactiveCommand.Create(() =>
        {
            Context.UndoChanges();
            DialogHost.Close(EntityPage, false);
        }, outputScheduler: Scheduler.CurrentThread);
    }
}