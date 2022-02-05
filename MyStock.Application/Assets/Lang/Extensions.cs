namespace MyStock.Application.Assets.Lang;

internal static class Extensions
{
    public static string Format(this string template, params object[] args) => string.Format(template, args);
}
