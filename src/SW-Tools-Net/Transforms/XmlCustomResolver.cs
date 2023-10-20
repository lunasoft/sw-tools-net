using System.Xml;

namespace SW.Tools.Transforms;

internal class XmlCustomResolver : XmlResolver
{
    private readonly XslBaseTransform _transform;
    internal XmlCustomResolver(XslBaseTransform transform)
    {
        _transform = transform;
    }
    public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
    {
        string filename = Path.GetFileName(absoluteUri.LocalPath);
        var name = _transform.GetAllResources().FirstOrDefault(l => l.Contains(filename));
        return _transform.GetAssembly().GetManifestResourceStream(name);
    }
}
