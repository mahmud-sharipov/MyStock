﻿using AutoMapper;

namespace MyStock.Core;

public abstract class EntityViewModel<TEntity, TValidator> : ViewModel, IEntityViewModel<TEntity>, INotifyDataErrorInfo
    where TEntity : class, IEntity
    where TValidator : class, IValidator
{
    private TValidator _validator;
    private IEnumerable<ValidationFailure> Errors;
    private bool _hasErrors;
    private bool _isValid;
    protected IObservable<bool> isValidObservable;
    protected EntityViewModel(Guid guid, IContext context) : base(context)
    {
        SharedConstructor(context.Get<TEntity>(guid));
    }

    protected EntityViewModel(TEntity entity, IContext context) : base(context)
    {
        SharedConstructor(entity);
    }

    void SharedConstructor(TEntity entity)
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
        var validationResult = _validator.Validate(this);
        return validationResult.IsValid;
    }

    public virtual void ActionBeforeSave()
    {
        Mapper.Map(this, Entity);
    }
    #endregion

    #region Raise methods
    protected void RaiseValidation(params string[] propertyName)
    {
        var result = _validator.Validate(this);
        Errors = result.Errors;
        HasErrors = result.Errors.Count > 0;
        IsValid = result.IsValid;
        foreach (var item in propertyName)
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(item));
    }

    protected void RaisePropertyChanged(string[] dependentProperties)
    {
        for (int i = 0; i < dependentProperties.Length; i++)
            this.RaisePropertyChanged(dependentProperties[i]);
    }

    protected bool RaiseAndSetIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
    {
        _ = propertyName ?? throw new ArgumentNullException(nameof(propertyName));

        if (EqualityComparer<T>.Default.Equals(backingField, newValue))
            return false;

        this.RaisePropertyChanging(propertyName);
        backingField = newValue;
        this.RaisePropertyChanged(propertyName);
        return true;
    }

    protected bool RaiseAndSetIfChanged<T>(ref T backingField, T newValue, string propertyName = null, params string[] dependentProperties)
    {
        var result = RaiseAndSetIfChanged(ref backingField, newValue, propertyName);
        if (result)
            RaisePropertyChanged(dependentProperties);
        return result;
    }

    protected bool RaiseAndSetAndValidateIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
    {
        var result = RaiseAndSetIfChanged(ref backingField, newValue, propertyName);
        if (result)
            RaiseValidation(propertyName);

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