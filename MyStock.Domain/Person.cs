namespace MyStock.Domain;

public class Person : EntityBase
{
    private string _firstName;
    private string _lastName;
    private string _middleName;
    private string _address;
    private string _phone;
    private bool _isActive;

    public string FirstName
    {
        get => _firstName;
        set
        {
            SetProptery(ref _firstName, value);
            RaisePropertyChanged(nameof(FullName));
        }
    }
    public string LastName
    {
        get => _lastName;
        set
        {
            SetProptery(ref _lastName, value);
            RaisePropertyChanged(nameof(FullName));
        }
    }
    public string MiddleName
    {
        get => _middleName; set
        {
            SetProptery(ref _middleName, value);
            RaisePropertyChanged(nameof(FullName));
        }
    }

    public string Address { get => _address; set => SetProptery(ref _address, value); }
    public virtual string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
    public string Phone { get => _phone; set => SetProptery(ref _phone, value); }
    public virtual string Code => $"{LastName?.FirstOrDefault()}{FirstName?.FirstOrDefault()}";
    public bool IsActive { get => _isActive; set => SetProptery(ref _isActive, value); }

    public override string ToString()
    {
        return FullName;
    }
}
