using SW.Tools.Commons.Enum;

namespace SW.Tools.Entities.Response;

public class BaseResponse<T>
{
    public string Status { get; private set; }
    public string Message { get; private set; }
    public T Data { get; private set; }

    protected internal BaseResponse() { }

    internal void SetStatus(ResponseStatus status)
    {
        this.Status = status.ToString();
    }
    internal void SetMessage(string message) 
    {
        this.Message = message != null || !String.IsNullOrEmpty(message) ? message 
            : throw new ArgumentException("Son requeridos las parámetros status y message.", nameof(message));
    }
    internal void SetData(T data)
    {
        Data = data != null ? data : throw new ArgumentException("El parámetro data es requerido.", nameof(data));
    }
}
