using Chilkat;
using SW.Tools.Validations;
using System.Security.Cryptography.X509Certificates;


namespace SW.Tools.Services.Certificate;

public class CertificateService
{
    protected internal CertificateService() { }

    internal byte[] CertificatePfxService(byte[] publicCertificate, byte[] privateKey, string password)
    {
        try
        {
            ValidationCertificate.ValidateParams(publicCertificate, privateKey, password); 

            Cert cert = new();
            if (!cert.LoadFromBinary(publicCertificate) || cert.CertVersion == 0)
            {
                throw new Exception("El certificado debe ser de extensión cer o crt.");
            }
            CertChain certChain = cert.GetCertChain();

            var privKey = new PrivateKey();
            if (!privKey.LoadPkcs8Encrypted(privateKey, password))
            {
                throw new Exception("La llave privada es incorrecta o la contraseña es invalida.");
            }

            Pfx pfx = new();
            pfx.AddPrivateKey(privKey, certChain);
            byte[] contenidoPfx = pfx.ToBinary(password);

            return contenidoPfx;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    internal void ReadPfxService(byte[] pfxBytes, string password, out byte[] publicCert, out byte[] privateCert)
    {
         if (pfxBytes == null || pfxBytes.Length == 0 || string.IsNullOrEmpty(password))
        {
            throw new Exception("Los parámetros PFX y contraseña son requeridos."); 
        }

        try
        {
            X509Certificate2 certificate = new(pfxBytes, password, X509KeyStorageFlags.Exportable);

            if (!certificate.HasPrivateKey)
            {
                throw new Exception("El archivo PFX es incorrecto o la contraseña es invalida.");
            }    
                
            publicCert = certificate.Export(X509ContentType.Cert);
            privateCert = certificate.Export(X509ContentType.Pfx, password);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
