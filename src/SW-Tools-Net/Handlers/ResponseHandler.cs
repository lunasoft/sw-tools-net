using SW.Tools.Commons.Enum;
using SW.Tools.Entities.Response;

namespace SW.Tools.Handlers;

internal class ResponseHandler<TResponse, TData> : IResponseHandler<TResponse, TData> where TResponse : BaseResponse<TData>, new()
{
    public TResponse SuccessResponse(TData responseData)
    {
        TResponse response = new();
        response.SetStatus(ResponseStatus.success);
        response.SetData(responseData);
        return response;
    }

    public TResponse ErrorResponse(Exception ex)
    {
        TResponse response = new();
        response.SetStatus(ResponseStatus.error);
        response.SetMessage(ex.Message);
        return response;
    }
}
