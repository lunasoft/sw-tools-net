using System.Xml;

namespace SW.Tools.UnitTest.Helpers;

internal class ResourceHelper
{
    internal static byte[] GetCertResource(string filename)
    {
        return File.ReadAllBytes($"Resources/Certificates/ut_{filename}");
    }
    internal static string GetInvoice(string filename)
    {
        var xml = File.ReadAllText($"Resources/Xml/ut_{filename}");

        XmlDocument xmlDoc = new();
        xmlDoc.LoadXml(xml);

        xmlDoc.DocumentElement.SetAttribute("Serie", "SW-Tools-Net");
        xmlDoc.DocumentElement.SetAttribute("Folio", Guid.NewGuid().ToString());
        xmlDoc.DocumentElement.SetAttribute("Fecha", DateTime.Now.AddHours(-2).ToString("yyyy-MM-ddTHH:mm:ss"));

        return xmlDoc.OuterXml;
    }
}