namespace MyStock.Domain;

public class Person : EntityBase
{
    private string firstName;
    private string lastName;
    private string middleName;
    private string address;
    private string phone;

    public string FirstName
    {
        get => firstName; set
        {
            SetProptery(ref firstName, value);
            RaisePropertyChanged(nameof(FullName));
        }
    }

    public string LastName
    {
        get => lastName;
        set
        {
            SetProptery(ref lastName, value);
            RaisePropertyChanged(nameof(FullName));
        }
    }
    public string MiddleName
    {
        get => middleName; set
        {
            SetProptery(ref middleName, value);
            RaisePropertyChanged(nameof(FullName));
        }
    }

    public string Address { get => address; set => SetProptery(ref address, value); }
    public virtual string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
    public string Phone { get => phone; set => SetProptery(ref phone, value); }

    public override string ToString()
    {
        return FullName;
    }
}
