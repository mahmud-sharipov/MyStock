namespace MyStock.Application.Uoms;

public interface IUomViewModel : IEntityViewModel<Uom>, IViewable
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
}
