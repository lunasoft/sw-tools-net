namespace SW.Tools.UnitTest.Helpers;

internal class BuildTest
{
    internal static string UrlService = "https://services.test.sw.com.mx";
    internal static string Token = Environment.GetEnvironmentVariable("SDKTEST_TOKEN");
    internal static byte[] publicCertificate = File.ReadAllBytes(@"Resources\Certificates\ut_csd_h&e951128469.cer");
    internal static byte[] privateKey = File.ReadAllBytes(@"Resources\Certificates\ut_csd_h&e951128469.key");
    internal static byte[] pfx = File.ReadAllBytes(@"Resources\Certificates\ut_pfx_h&e951128469.pfx");
    internal static string passwordCer = "12345678a";
}