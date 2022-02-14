

using MyStock.Application.Assets.Lang;

namespace MyStock.Pages.Customers;

internal class CustomerFullTextSearch : FullTextSearch
{
    protected override string GetSubtitlePropertyName() => nameof(Customer.Phone);

    protected override PackIconMaterialKind GetTileIcon() => PackIconMaterialKind.AccountCircle;

    protected override string GetTitlePropertyName() => nameof(Customer.FullName);
}
