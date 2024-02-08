namespace SW.Tools.Entities.Response;

public class PfxResponse : BaseResponse<PfxResponseData>
{
}
public class PfxResponseData
{
    public byte[] PublicCert { get; private set; }
    public byte[] PrivateCert { get; private set; }
    internal PfxResponseData(byte[] publicCert, byte[] privateCert)
    {
        PublicCert = publicCert;
        PrivateCert = privateCert;
    }
}