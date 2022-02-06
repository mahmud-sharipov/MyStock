using MyStock.Application.Uoms;
using MyStock.Application.Uoms.Pages;

namespace MyStock.Pages.Uoms
{
    public partial class UomPage : IUomEntityPage
    {
        public UomPage(UomViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
