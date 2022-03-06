namespace MyStock.Core;

public class Global
{
    static Settings _settings;
    static IContext _context;

    public static Autofac.IContainer Container { get; set; }

    public static IContext Context =>
       _context ??= Container.Resolve<IContext>();

    public static Settings Settings =>
        _settings ??= Context.Set<Settings>().Single();
}
