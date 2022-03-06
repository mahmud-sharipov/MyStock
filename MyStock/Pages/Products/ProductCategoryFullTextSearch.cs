namespace MyStock.Pages.Products;

internal class ProductCategoryFullTextSearch : FullTextSearch
{
    protected override string GetTitlePropertyName() => nameof(ProductCategory.Name);

    protected override string GetSubtitlePropertyName() => nameof(ProductCategory.Name);

    protected override PackIconMaterialKind GetTileIcon() => PackIconMaterialKind.Sitemap;
}
