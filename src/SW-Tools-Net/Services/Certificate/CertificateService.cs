using SW.Tools.Helpers;
using SW.Tools.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.X509;

namespace SW.Tools.Services.Certificate;

public class CertificateService
{
    protected internal CertificateService() { }

    internal byte[] CertificatePfxService(byte[] bytesCER, byte[] bytesKEY, string password)
    {
        try
        {
            var certificate = new X509Certificate2(bytesCER, password);

            char[] arrayOfChars = password.ToCharArray();
            AsymmetricKeyParameter privateKey = PrivateKeyFactory.DecryptKey(arrayOfChars, bytesKEY);
            RSACryptoServiceProvider subjectKey = CertificateUtils.ToRSA((RsaPrivateCrtKeyParameters)privateKey);
            var PrivateKey = certificate.CopyWithPrivateKey(subjectKey);
            return certificate.Export(X509ContentType.Pfx, password);
        }
        catch (Exception ex)
        {
            throw new Exception("Los datos del Certificado CER KEY o Password son incorrectos. No es posible leer la llave privada.", ex);
        }
    }
}
