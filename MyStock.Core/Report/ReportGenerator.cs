using NReco.PdfGenerator;

namespace MyStock.Core.Report;

public class ReportGenerator
{
    private static ReportGenerator instance = null;
    private static readonly object padlock = new object();
    private readonly HtmlToPdfConverter _htmlToPdfConverter;

    ReportGenerator()
    {
        _htmlToPdfConverter = new HtmlToPdfConverter();

        //Hack the License
        var filedI = typeof(LicenseInfo).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public).First();
        var filedOwner = filedI.FieldType.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public).First(f => f.Name == "Owner");
        var filedIsLicensed = filedI.FieldType.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public).First(f => f.Name == "IsLicensed");
        var info = Activator.CreateInstance(filedI.FieldType, true);
        filedOwner.SetValue(info, "My_hack_key");
        filedIsLicensed.SetValue(info, true);
        filedI.SetValue(_htmlToPdfConverter.License, info);


        var psi = new System.Diagnostics.ProcessStartInfo();
        psi.UseShellExecute = true;
        psi.FileName = @"D:/Test.pdf";
        System.Diagnostics.Process.Start(psi);
    }

    internal static ReportGenerator Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ReportGenerator();
                }
                return instance;
            }
        }
    }

    public void Generate(string html, string outputPath)
    {
        _htmlToPdfConverter.GeneratePdf(html, null, outputPath);
    }
}

public class ReportBuilder
{
    string _reportHtml;

    public ReportBuilder(string repotFilePath)
    {
        _reportHtml = File.ReadAllText(repotFilePath);
    }

    public static ReportBuilder New(string repotFilePath)
    {
        return new ReportBuilder(repotFilePath);
    }

    public ReportBuilder AddParameter(string name, string value)
    {
        _reportHtml = _reportHtml.Replace(name, value);
        return this;
    }

    public ReportBuilder UseDefaultStyles()
    {
        var css = $"<style>{File.ReadAllText(ReportGlobalConsts.BootstrapCssPath)}</style><style>{File.ReadAllText(ReportGlobalConsts.DefaultCssPath)}</style>";
        return AddParameter(ReportGlobalConsts.StylesParamter, css);
    }

    public ReportBuilder Generate(string outputPath)
    {
        ReportGenerator.Instance.Generate(_reportHtml, outputPath);
        return this;
    }

    public ReportBuilder GenerateAndOpen(string outputPath)
    {
        Generate(outputPath);
        var psi = new System.Diagnostics.ProcessStartInfo();
        psi.UseShellExecute = true;
        psi.FileName = outputPath;
        System.Diagnostics.Process.Start(psi);
        return this;
    }
}

public static class ReportGlobalConsts
{
    public const string StylesParamter = "$Styles$";
    public const string BootstrapCssPath = "Assets/Dist/bootstrap.min.css";
    public const string DefaultCssPath = "Assets/Dist/report.styles.css";

}
