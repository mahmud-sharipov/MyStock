using MyStock.Core.Interfaces;

namespace MyStock.Pages;

public class EntityPage<TViewModel> : BasePage<TViewModel>, IEntityPage
    where TViewModel : class, IViewable
{
    public EntityPage(TViewModel viewModel) : base(viewModel) { }

    IViewable IEntityPage.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as TViewModel;
    }
}
