using SW.Tools.Entities.Response;
using SW.Tools.Handlers;

namespace SW.Tools.Services.Sign;

public class Sign : SignService, ISign<SignResponse>
{
    private readonly SignServiceHandler _handler;
    /// <summary>
    /// Contiene métodos para realizar el sellado de comprobantes CFDI 4.0 y Retencion 2.0, 
    /// así como para generar y agregar el signature a un XML.
    /// </summary>
    public Sign()
    {
        _handler = new();
    }

    /// <summary>
    /// Sella un comprobante CFDI 4.0 en formato XML, actualiza los campos Sello, NoCertificado y Certificado.
    /// </summary>
    /// <returns>Un objeto SignResponse que contiene el XML del CFDI sellado.</returns>
    public SignResponse SignCfdi(string xml, byte[] pfx, string password)
    {
        return _handler.Execute(() => SignInvoiceService(xml, pfx, password));
    }

    /// <summary>
    /// Sella un comprobante Retencion 2.0 en formato XML, actualiza los campos Sello, NoCertificado y Certificado.
    /// </summary>
    /// <returns>Un objeto SignResponse que contiene el XML de Retencion sellado.</returns>
    public SignResponse SignRetention(string xml, byte[] pfx, string password)
    {
        return _handler.Execute(() => SignInvoiceService(xml, pfx, password, true));
    }

    /// <summary>
    /// Genera y agrega el signature a un documento XML, puede usarse para firmar XML de cancelación, de aceptacion de cancelación, entre otros.
    /// </summary>
    /// <returns>Un objeto SignResponse que contiene el XML firmado.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public SignResponse SignXml(string xml, byte[] pfx, string password)
    {
        return _handler.Execute(() => SignatureService(xml, pfx, password));
        //throw new NotImplementedException();
    }
}
