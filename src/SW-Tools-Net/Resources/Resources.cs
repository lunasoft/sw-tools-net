using System.Reflection;

namespace SW.Tools.Resources;

internal class Resources
{
    private readonly List<string> _resources;
    private readonly Assembly _assembly;
    internal byte[] GetResource(string resourceName)
    {
        using var rs = _assembly.GetManifestResourceStream(_resources.FirstOrDefault(l => l.Contains(resourceName)));
        using MemoryStream ms = new();
        rs.CopyTo(ms);
        return ms.ToArray();
    }
    internal List<string> GetAllResources() => _resources;
    internal Assembly GetAssembly() => _assembly;
    internal Resources()
    {
        _assembly = Assembly.GetAssembly(typeof(Resources));
        _resources = _assembly.GetManifestResourceNames().ToList();
    }
}