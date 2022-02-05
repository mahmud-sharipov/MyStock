using MyStock.Application.Uoms;
using MyStock.Application.Uoms.Pages;

namespace MyStock.Pages.Uoms
{
    public partial class UomListPage : IUomListEntityPage
    {
        public UomListPage(UomListViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            SetupCollection(dataGrid);
        }
    }
}
