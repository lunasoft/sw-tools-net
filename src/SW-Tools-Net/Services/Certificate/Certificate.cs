using SW.Tools.Entities.Response;
using SW.Tools.Handlers;
using SW.Tools.Commons.Enum;

namespace SW.Tools.Services.Certificate;

public class Certificate : CertificateService, ICertificate<CertificateResponse> 
{
    private readonly CertificateServiceHandler _handler;
    /// <summary>
    /// Contiene un método para realizar la creación de un PFX en formato de bytes.
    /// </summary>
    public Certificate()
    {
        _handler = new CertificateServiceHandler();
    }

    /// <summary>
    /// Crea un PFX a partir de un archivo .Cer, .key y su respectiva contraseña.
    /// </summary>
    /// <returns>Un objeto CertificateResponse que contiene el PFX creado en formato de bytes.</returns>
    public CertificateResponse CreatePfx(byte[] publicCertificate, byte[] privateKey, string password)
    {
        var response = new CertificateResponse();
        try
        {
            byte[] pfxBytes = CertificatePfxService(publicCertificate, privateKey, password);
                        
            response.SetData(new CertificateResponseData(pfxBytes));
            response.SetStatus(ResponseStatus.success);
        }
        catch (Exception ex)
        {
            response.SetMessage(ex.Message);
            response.SetStatus(ResponseStatus.error);
        }
        return response;
    }

    /// <summary>
    /// Lee un archivo PFX y su respectiva contraseña para recuperar el certificado publico y privado.
    /// </summary>
    /// <returns>Un objeto CertificateResponse que contiene el certificado publico y privado en formato de bytes.</returns>
    public CertificateResponse ReadPfx(byte[] pfxBytes, string password)
    {
        var response = new CertificateResponse();
        try
        {
            byte[] publicCert;
            byte[] privateCert;

            ReadPfxService(pfxBytes, password, out publicCert, out privateCert);

            response.SetData(new CertificateResponseData(publicCert, privateCert));
            response.SetStatus(ResponseStatus.success);
        }
        catch (Exception ex)
        {
            response.SetMessage(ex.Message);
            response.SetStatus(ResponseStatus.error);
        }
        return response;
    }
}
