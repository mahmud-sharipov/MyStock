namespace MyStock.Pages.Products;

internal class ProductFullTextSearch : FullTextSearch
{
    protected override string GetTitlePropertyName() => nameof(Product.Description);

    protected override string GetSubtitlePropertyName() => nameof(Product.Code);

    protected override PackIconMaterialKind GetTileIcon() => PackIconMaterialKind.PackageVariant;
}
