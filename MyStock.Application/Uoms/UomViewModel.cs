using MyStock.Application.Uoms.Pages;
using MyStock.Application.Uoms.Validators;

namespace MyStock.Application.Uoms;

public class UomViewModel : EntityPageViewModel<Uom, UomValidator, IUomEntityPage>, IUomViewModel
{
    private string _name;
    private string _code;
    private string _description;

    public UomViewModel(Uom entity, IContext context) : base(entity, context) { }

    public string Name
    {
        get => _name;
        set => RaiseAndSetAndValidateIfChanged(ref _name, value);
    }

    public string Code
    {
        get => _code;
        set => RaiseAndSetAndValidateIfChanged(ref _code, value);
    }

    public string Description
    {
        get => _description;
        set => RaiseAndSetIfChanged(ref _description, value);
    }
}
