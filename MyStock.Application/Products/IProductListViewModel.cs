using MyStock.Application.Products.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStock.Application.Products
{
    public interface IProductListViewModel : IEntityListPageViewModel<Product>
    {
        string NameSearchText { get; set; }
    }

    public class ProductListViewModel : EntityListPageViewModel<Product, IProductListEntityPage>, IProductListViewModel
    {
        private string nameSearchText;

        public ProductListViewModel(IContext context) : base(context)
        {
            Title = Translations.Products;
        }

        public string NameSearchText
        {
            get => nameSearchText;
            set => this.RaiseAndSetIfChanged(ref nameSearchText, value, nameof(NameSearchText), nameof(Collection));
        }

        public override bool CanDeleteEntity(Product entity, out string reason)
        {
            reason = "";
            if (entity.DocumentDetails.Any())
            {
                reason = Translations.ProductHasDocumentMessage;
                return false;
            }
            return true;
        }

        protected override ICollection<ColumnViewModel> BuildColums()
        {
            return new List<ColumnViewModel>()
            {
                new ColumnViewModel(Translations.Name, nameof(IProductViewModel.Name),1),
                new ColumnViewModel(Translations.Code, nameof(IProductViewModel.Code),2),
                new ColumnViewModel(Translations.Description, nameof(IProductViewModel.Description),3),
            };
        }

        protected override IViewable CreateEntityViewModel(Product entity) => new ProductViewModel(entity, Context);

        protected override bool FilereItem(Product entity)
        {
            return string.IsNullOrEmpty(NameSearchText) || entity.Description.Contains(NameSearchText, StringComparison.CurrentCultureIgnoreCase);
        }
    }

}
