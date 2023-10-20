using SW.Tools.Helpers;
using SW.Tools.Validations;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace SW.Tools.Services.Sign;

public class SignService
{
    protected internal SignService() { }

    internal string SignInvoiceService(string xml, byte[] pfx, string password, bool isRetention = false)
    {
        try
        {
            Validation.ValidateSignParams(xml, pfx, password);

            XmlDocument cfdiDoc = new();
            cfdiDoc.LoadXml(xml);

            string tag = cfdiDoc.DocumentElement.Name;
            Validation.ValidateInvoice(tag, isRetention);

            string version = cfdiDoc.DocumentElement.GetAttribute("Version");
            Validation.ValidateInvoiceVersion(tag, version, isRetention);

            X509Certificate2 x509Certificate = new(pfx, password);
            var (b64Certificate, certificateNumber) = CryptoHelper.GetCertificateValues(x509Certificate);
            cfdiDoc.DocumentElement.SetAttribute("NoCertificado", certificateNumber);
            cfdiDoc.DocumentElement.SetAttribute("Certificado", b64Certificate);
            string originalChain = CryptoHelper.GetOriginalString(cfdiDoc.OuterXml, isRetention);
            string hashedString = CryptoHelper.GetHashedStringSha256(originalChain, x509Certificate);
            cfdiDoc.DocumentElement.SetAttribute("Sello", hashedString);

            return cfdiDoc.OuterXml;
        }
        catch(XmlException ex) 
        {
            throw new Exception($"El XML no es válido.{ex.Message}");
        }
        catch(CryptographicException ex) 
        {
            throw new Exception($"El certificado no es un PFX válido.{ex.Message}");
        }
    }
}
