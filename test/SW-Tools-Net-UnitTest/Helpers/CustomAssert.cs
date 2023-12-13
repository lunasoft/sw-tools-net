using SW.Services.AcceptReject;
using SW.Services.Cancelation;
using SW.Services.Stamp;
using SW.Tools.Commons.Enum;
using SW.Tools.Entities.Response;
using System.Text;
using Xunit.Sdk;

namespace SW.Tools.UnitTest.Helpers;

internal class CustomAssert
{
    private Cancelation cancellation;
    private AcceptReject acceptReject;
    internal static void ResultIsSuccess<T>(BaseResponse<T> result)
    {
        if (result == null || string.IsNullOrEmpty(result.Status) || result.Data == null) 
            throw NotNullException.ForNullValue();
        bool condition = result.Status.Equals(ResponseStatus.success.ToString());
        if (!condition) 
            throw TrueException.ForNonTrueValue($"Se esperaba Status igual a {ResponseStatus.success} pero fue {result.Status}", condition);
    }
    internal static void ResultIsError<T>(BaseResponse<T> result, string expectedMessage = null)
    {
        if (result == null || string.IsNullOrEmpty(result.Status) || string.IsNullOrEmpty(result.Message))
            throw NotNullException.ForNullValue();
        bool condition = result.Status.Equals(ResponseStatus.error.ToString());
        if (!condition) 
            throw TrueException.ForNonTrueValue($"Se esperaba {ResponseStatus.error} pero fue {result.Status}", condition);
        condition = !string.IsNullOrEmpty(result.Message);
        if (!condition) 
            throw TrueException.ForNonTrueValue($"Se esperaba que Message tuviera un valor pero fue {result.Message}", condition);
        if (expectedMessage != null)
        {
            condition = result.Message.Equals(expectedMessage);
            if (!condition)
                throw TrueException.ForNonTrueValue($"Se esperaba Message igual a {expectedMessage} pero fue {result.Message}", condition);
        }
    }
    internal static void StampIsSuccess(Stamp stamp, string xml)
    {
        var result = stamp.TimbrarV1Async(xml).Result;
        var condition = result != null && result.Status.Equals(ResponseStatus.success.ToString());
        if (!condition)
            throw TrueException.ForNonTrueValue("El timbrado no fue exitoso.", condition);
    }
    internal void CancellationAcceptRejectIsSuccess(string xml, bool isCancellation = false)
    {
        acceptReject = new(BuildTest.UrlService, BuildTest.Token);
        cancellation = new(BuildTest.UrlService, BuildTest.Token);

        if (isCancellation)
        {
            var result = cancellation.CancelarByXMLAsync(Encoding.UTF8.GetBytes(xml)).Result;
            var condition = result != null && result.Status.Equals(ResponseStatus.success.ToString());
            if (!condition)
                throw TrueException.ForNonTrueValue("Cancelación no fue exitoso.", condition);
        }
        else
        {
            var result = acceptReject.AcceptByXML(Encoding.UTF8.GetBytes(xml)).Result;
            var condition = result != null && result.Status.Equals(ResponseStatus.success.ToString());
            if (!condition)
                throw TrueException.ForNonTrueValue("AceptacionRechazo no fue exitoso.", condition);
        }
    }
}
