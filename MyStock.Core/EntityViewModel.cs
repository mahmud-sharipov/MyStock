using AutoMapper;

namespace MyStock.Core;

public abstract class EntityViewModel<TEntity, TValidator> : ViewModel, IEntityViewModel<TEntity>, INotifyDataErrorInfo
    where TEntity : class, IEntity
    where TValidator : class, IValidator
{
    TValidator _validator;
    IEnumerable<ValidationFailure> Errors;
    bool _hasErrors;
    bool _isValid;
    protected IObservable<bool> isValidObservable;

    protected EntityViewModel(TEntity entity, IContext context) : base(context)
    {
        isValidObservable = this.WhenAny(v => v.IsValid, val => val.Value);
        _validator = Global.Container.Resolve<TValidator>();
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        Mapper = Global.Container.Resolve<IMapper>();
        Mapper.Map(Entity, this);
        InitializeAssociatedProperties();
        BuildCommands();
    }

    public IMapper Mapper { get; protected set; }

    public TEntity Entity { get; protected set; }

    public ICommand SaveChange { get; protected set; }

    protected virtual void InitializeAssociatedProperties() { }

    protected virtual void BuildCommands()
    {
        SaveChange = ReactiveCommand.Create(() =>
        {
            if (ValidateBeforeSaveChange())
            {
                ActionBeforeSave();
                Context.SaveChanges();
            }
        }, isValidObservable, outputScheduler: Scheduler.CurrentThread);
    }

    #region public methods
    public virtual bool ValidateBeforeSaveChange()
    {
        RaiseValidation();
        return IsValid;
    }

    public virtual void ActionBeforeSave()
    {
        Mapper.Map(this, Entity);
    }
    #endregion

    #region Raise methods
    protected void RaiseValidation()
    {
        var result = _validator.Validate(this);
        var currentErrors = Errors ?? Enumerable.Empty<ValidationFailure>();
        Errors = result.Errors;
        HasErrors = result.Errors.Count > 0;
        IsValid = result.IsValid;

        foreach (var item in currentErrors)
        {
            if (!Errors.Any(er => er.PropertyName == item.PropertyName))
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(item.PropertyName));
        }

        foreach (var item in Errors.Select(e => e.PropertyName).Distinct())
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(item));
    }

    protected bool RaiseAndSetAndValidateIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
    {
        var result = RaiseAndSetIfChanged(ref backingField, newValue, propertyName);
        if (result)
            RaiseValidation();

        return result;
    }

    protected bool RaiseAndSetAndValidateIfChanged<T>(ref T backingField, T newValue, string propertyName = null, params string[] dependentProperties)
    {
        var result = RaiseAndSetAndValidateIfChanged(ref backingField, newValue, propertyName);
        if (result)
            RaisePropertyChanged(dependentProperties);

        return result;
    }
    #endregion

    #region INotifyDataErrorInfo

    public bool IsValid
    {
        get => _isValid;
        private set => RaiseAndSetIfChanged(ref _isValid, value);
    }

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public bool HasErrors { get => _hasErrors; private set => RaiseAndSetIfChanged(ref _hasErrors, value); }


    public IEnumerable GetErrors(string propertyName)
       => Errors?.Where(x => x.PropertyName == propertyName) ?? Enumerable.Empty<ValidationFailure>();

    #endregion
}