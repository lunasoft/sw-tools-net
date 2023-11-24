using SW.Tools.Entities.Response;

namespace SW.Tools.Handlers;

internal class CertificateServiceHandler
{
    private readonly IResponseHandler<CertificateResponse,CertificateResponseData> _handler;
    internal CertificateServiceHandler()
    {
        _handler = new ResponseHandler<CertificateResponse,CertificateResponseData>();
    }
    internal CertificateResponse Execute(Func<byte[]> service)
    {
        try
        {
            CertificateResponseData response = new(service());
            return _handler.SuccessResponse(response);
        }
        catch (Exception ex)
        {
            return _handler.ErrorResponse(ex);
        }
    }
}
