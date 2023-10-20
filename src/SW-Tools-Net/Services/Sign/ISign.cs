
namespace SW.Tools.Services.Sign
{
    internal interface ISign<T>
    {
        T SignCfdi(string xml, byte[] pfx, string password);
        T SignXml(string xml, byte[] pfx, string password);
        T SignRetention(string xml, byte[] pfx, string password);
    }
}
