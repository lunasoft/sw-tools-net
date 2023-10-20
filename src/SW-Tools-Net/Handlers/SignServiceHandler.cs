using SW.Tools.Entities.Response;

namespace SW.Tools.Handlers;

internal class SignServiceHandler
{
    private readonly IResponseHandler<SignResponse, SignResponseData> _handler;
    internal SignServiceHandler()
    {
        _handler = new ResponseHandler<SignResponse, SignResponseData>();
    }
    internal SignResponse Execute(Func<string> service)
    {
        try
        {
            SignResponseData response = new(service());
            return _handler.SuccessResponse(response);
        }
        catch (Exception ex)
        {
            return _handler.ErrorResponse(ex);
        }
    }
}
