namespace SW.Tools.Entities.Response;

public class CertificateResponse : BaseResponse<CertificateResponseData>
{
}
public class CertificateResponseData
{
    public byte[] Pfx { get; private set; }
    public byte[] PublicCert { get; private set; }
    public byte[] PrivateCert { get; private set; }
    
    public CertificateResponseData(byte[] publicCert, byte[] privateCert)
    {
        PublicCert = publicCert;
        PrivateCert = privateCert;
    }
    internal CertificateResponseData(byte[] pfx)
    {
        Pfx = pfx;
    }
}
