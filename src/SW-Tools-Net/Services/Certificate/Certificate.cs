using SW.Tools.Entities.Response;
using SW.Tools.Handlers;

namespace SW.Tools.Services.Certificate;

public class Certificate : CertificateService, ICertificate<CertificateResponse> 
{
    private readonly CertificateServiceHandler _handler;
    /// <summary>
    /// Contiene un método para realizar la creación de un PFX en formato de bytes.
    /// </summary>
    public Certificate()
    {
        _handler = new();
    }

    /// <summary>
    /// Crea un PFX a partir de un archivo .Cer, .key y su respectiva contraseña.
    /// </summary>
    /// <returns>Un objeto CertificateResponse que contiene el PFX creado en formato de bytes.</returns>
    public CertificateResponse CreatePfx(byte[] bytesCER, byte[] bytesKEY, string password)
    {
        return _handler.Execute(() => CertificatePfxService(bytesCER, bytesKEY, password));
    }
}
