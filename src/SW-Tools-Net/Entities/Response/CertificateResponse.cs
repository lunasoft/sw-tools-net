namespace SW.Tools.Entities.Response;

public class CertificateResponse : BaseResponse<CertificateResponseData>
{
}
public class CertificateResponseData
{
    public byte[] Pfx { get; private set; }
    internal CertificateResponseData(byte[] pfx)
    {
        Pfx = pfx;
    }
}
