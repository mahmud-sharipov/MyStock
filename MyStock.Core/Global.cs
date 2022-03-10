namespace MyStock.Core;

public static class Global
{
    static Settings _settings;
    static IContext _context;

    public static Autofac.IContainer Container { get; set; }

    public static IContext Context =>
       _context ??= Container?.Resolve<IContext>();

    public static Settings Settings =>
#if DEBUG
        _settings ??= Context?.Set<Settings>().Single() ?? new Settings() { CompanyName = "MyStocl", Lagnuage = "ru-RU" };
#else
        _settings ??= Context.Set<Settings>().Single();
#endif
    public static User CurrentUser { get; internal set; }

    public static string DateFormate => "ddd, MMM d, yyyy";
}
