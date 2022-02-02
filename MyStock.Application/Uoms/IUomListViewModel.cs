namespace MyStock.Application.Uoms
{
    public interface IUomListViewModel : IEntityListPageViewModel<Uom>
    {

    }

    public class UomListViewModel : EntityListPageViewModel<Uom>, IUomListViewModel
    {
        public UomListViewModel(IContext context) : base(context)
        {
        }

        public override void Delete(Uom entity)
        {
            throw new NotImplementedException();
        }

        protected override ICollection<ColumnViewModel> BuildColums()
        {
            return new List<ColumnViewModel>()
            {
                new ColumnViewModel("Name",nameof(IUomViewModel.Name),1),
                new ColumnViewModel("Code",nameof(IUomViewModel.Code),2),
                new ColumnViewModel("Description",nameof(IUomViewModel.Description),3),
            };
        }

        protected override IViewable CreateEntityViewModel(Uom entity) => new UomViewModel(entity, Context);
    }

}
