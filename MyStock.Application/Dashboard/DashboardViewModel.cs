namespace MyStock.Application.Dashboard;
public class DashboardViewModel : ViewModel, IDashboardViewModel
{

    public DashboardViewModel(IContext context) : base(context)
    {

    }

    IDashboardPage _entityPage;
    public IDashboardPage EntityPage =>
        _entityPage ??= Global.Container.Resolve<IDashboardPage>(new PositionalParameter(0, this));

    IEntityListPage INavigatable.EntityPage => EntityPage;
}
