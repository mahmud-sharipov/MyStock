namespace MyStock.Pages.Vendors;

internal class VendorFullTextSearch : FullTextSearch
{
    protected override string GetSubtitlePropertyName() => nameof(Vendor.Phone);

    protected override PackIconMaterialKind GetTileIcon() => PackIconMaterialKind.AccountBox;

    protected override string GetTitlePropertyName() => nameof(Vendor.FullName);
}
