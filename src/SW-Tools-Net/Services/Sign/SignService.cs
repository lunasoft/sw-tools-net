using Chilkat;
using SW.Tools.Helpers;
using SW.Tools.Validations;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
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
            Validation.ValidateInvoice(tag);

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
    internal string SignatureService(string xml, byte[] pfx, string password)
    {
        try
        {
            XmlDocument cfdiDoc = new XmlDocument();
            cfdiDoc.LoadXml(xml);
            string tag = cfdiDoc.DocumentElement.Name;
            string xmlSignature = XmlHelper.RemoveSignatureNodes(xml);
            Validation.ValidateSignParams(xml, pfx, password);
            Validation.ValidateInvoice(tag);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlSignature);

            X509Certificate2 cert = new X509Certificate2(pfx, password);

            RSA key = cert.GetRSAPrivateKey();

            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = key;

            Reference reference = new Reference(string.Empty);
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());

            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(cert);
            keyInfoData.AddIssuerSerial(cert.IssuerName.Name, cert.SerialNumber);

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(keyInfoData);

            signedXml.KeyInfo = keyInfo;
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            XmlElement signatureElement = signedXml.GetXml();

            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(signatureElement, true));

            return XmlHelper.RemoveInvalidCharsXml(xmlDoc.OuterXml);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error durante la firma del XML. {ex.Message}");
        }
    }
   
}
