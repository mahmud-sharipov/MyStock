namespace MyStock.Pages.Uoms;

internal class UomFullTextSearch : FullTextSearch
{
    protected override string GetTitlePropertyName() => nameof(Uom.Name);

    protected override string GetSubtitlePropertyName() => nameof(Uom.Code);

    protected override PackIconMaterialKind GetTileIcon() => PackIconMaterialKind.Ruler;
}
