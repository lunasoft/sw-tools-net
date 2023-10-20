using System.Xml;
using System.Xml.Xsl;

namespace SW.Tools.Transforms;

internal class XslBaseTransform : Resources.Resources
{
    private readonly string _xml;
    private readonly XslCompiledTransform _xslTransformer;
    protected XslBaseTransform(string xml, string xsltResource) 
    { 
        _xml = xml;
        _xslTransformer = new();
        using var xsltReader = XmlReader.Create(GetAssembly()
        .GetManifestResourceStream(GetAllResources().FirstOrDefault(l => l.Contains(xsltResource))));
        _xslTransformer.Load(xsltReader, XsltSettings.TrustedXslt, new XmlCustomResolver(this));
    }
    internal string Transform()
    {
        using var sw = new StringWriter();
        using var xReader = XmlReader.Create(new StringReader(_xml));
        _xslTransformer.Transform(xReader, null, sw);
        string transformedXml = sw.ToString();
        return transformedXml;
    }
}