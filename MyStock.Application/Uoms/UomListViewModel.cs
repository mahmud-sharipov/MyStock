using MyStock.Application.Uoms.Pages;

namespace MyStock.Application.Uoms
{
    public class UomListViewModel : EntityListPageViewModel<Uom, IUomListEntityPage>, IUomListViewModel
    {
        public UomListViewModel(IContext context) : base(context)
        {
            Collection = new ObservableCollection<Uom>(Source.ToList());
            Title = Lang.UOMs;
        }

        public override void Delete(Uom entity)
        {
            throw new NotImplementedException();
        }

        protected override ICollection<ColumnViewModel> BuildColums()
        {
            return new List<ColumnViewModel>()
            {
                new ColumnViewModel("Name", nameof(IUomViewModel.Name),1),
                new ColumnViewModel("Code", nameof(IUomViewModel.Code),2),
                new ColumnViewModel("Description", nameof(IUomViewModel.Description),3),
            };
        }

        protected override IViewable CreateEntityViewModel(Uom entity) => new UomViewModel(entity, Context);
    }
}
