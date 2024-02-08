using SW.Tools.Entities.Response;

namespace SW.Tools.Handlers;

internal class CertificateServiceHandler
{
    private readonly IResponseHandler<CertificateResponse,CertificateResponseData> _handler;
    private readonly IResponseHandler<PfxResponse,PfxResponseData> _pfxHandler;
    internal CertificateServiceHandler()
    {
        _handler = new ResponseHandler<CertificateResponse,CertificateResponseData>();
        _pfxHandler = new ResponseHandler<PfxResponse,PfxResponseData>();
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
    internal PfxResponse Execute(Func<(byte[], byte[])> service)
    {
        try
        {
            PfxResponseData response = new(service().Item1, service().Item2);
            return _pfxHandler.SuccessResponse(response);
        }
        catch (Exception ex)
        {
            return _pfxHandler.ErrorResponse(ex);
        }
    } 
}
