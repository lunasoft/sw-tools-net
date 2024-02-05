using SW.Tools.Entities.Response;

namespace SW.Tools.Handlers;

internal class PfxServiceHandler
{
    private readonly IResponseHandler<PfxResponse,PfxResponseData> _handler;
    internal PfxServiceHandler()
    {
        _handler = new ResponseHandler<PfxResponse,PfxResponseData>();
    }
    internal PfxResponse Execute(Func<PfxResponseData> func)
    {
        try
        {
            PfxResponseData response = func();
            return _handler.SuccessResponse(response);
        }
        catch (Exception ex)
        {
            return _handler.ErrorResponse(ex);
        }
    }
}