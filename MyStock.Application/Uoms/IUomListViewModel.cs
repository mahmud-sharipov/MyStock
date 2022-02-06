namespace MyStock.Application.Uoms;

public interface IUomListViewModel : IEntityListPageViewModel<Uom>
{
    string NameSearchText { get; set; }
}
