using SW.Tools.Entities.Response;

namespace SW.Tools.Handlers;

internal interface IResponseHandler<TResponse, TData> where TResponse : BaseResponse<TData>
{
    TResponse SuccessResponse(TData responseData);
    TResponse ErrorResponse(Exception ex);
}
