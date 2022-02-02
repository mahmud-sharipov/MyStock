namespace MyStock.Core;

public abstract class ViewableEntityViewModel<TEntity, TValidator, TPage> : EntityViewModel<TEntity, TValidator>, IViewable
    where TEntity : class, IEntity
    where TValidator : class, IValidator
    where TPage : class, IEntityPage
{
    private IDialogHost _dialogHost;
    private TPage _entityPage;

    protected ViewableEntityViewModel(Guid guid, IContext context) : base(guid, context) { }

    protected ViewableEntityViewModel(TEntity entity, IContext context) : base(entity, context) { }

    public IDialogHost DialogHost =>
        _dialogHost ??= Global.Container.Resolve<IDialogHost>();

    public TPage EntityPage
    {
        get
        {
            if (_entityPage == null)
            {
                _entityPage = Global.Container.Resolve<TPage>();
                _entityPage.ViewModel = this;
            }
            return _entityPage;
        }
    }

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
                DialogHost.Close(EntityPage);
            }
        }, isValidObservable, outputScheduler: Scheduler.CurrentThread);

        Close = ReactiveCommand.Create(() =>
        {
            Context.UndoChanges();
            DialogHost.Close(EntityPage);
        }, outputScheduler: Scheduler.CurrentThread);
    }
}