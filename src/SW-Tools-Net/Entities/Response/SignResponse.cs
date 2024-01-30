namespace SW.Tools.Entities.Response;

public class SignResponse : BaseResponse<SignResponseData> 
{
}
public class SignResponseData
{
    public string Xml { get; private set; }
    internal SignResponseData(string xml)
    {
        Xml = !String.IsNullOrEmpty(xml) ? xml : throw new ArgumentNullException(nameof(xml));
    }
}