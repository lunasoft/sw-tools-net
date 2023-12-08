using Chilkat;
using SW.Tools.Helpers;
using SW.Tools.Validations;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
    internal string SignatureService(string xml, byte[] pfx, string password)
    {
        try
        {
            XmlDocument cfdiDoc = new();
            cfdiDoc.LoadXml(xml);
            string tag = cfdiDoc.DocumentElement.Name;
            string xmlSignature = XmlHelper.RemoveSignatureNodes(xml);
            Validation.ValidateSignParams(xml, pfx, password);
            Validation.ValidateInvoice(tag);

            Chilkat.Xml xmlDoc = new();
            xmlDoc.LoadXml(xmlSignature);
            Pfx objPfx = new();
            StringBuilder sbXml = new();
            if (!objPfx.LoadPfxBytes(pfx, password))
                throw new CryptographicException(objPfx.LastErrorText);
            var tagSignature = xmlDoc.GetRoot().Tag;
            if (String.IsNullOrEmpty(tagSignature) || tagSignature.Equals("empty"))
                throw new XmlException($"El elemento {tagSignature} del XML raíz no es válido.");
            var (signature, issuerName, serialNumber) = CryptoHelper.GetSignatureSha1(objPfx, tagSignature);
            if (!sbXml.Append(xmlSignature))
                throw new XmlException("Load XML Failed. " + signature.LastErrorText);
            if (!signature.CreateXmlDSigSb(sbXml))
                throw new Exception("Can't Sign Document. " + signature.LastErrorText);
            var signedXml = sbXml.GetAsString();
            xmlDoc.LoadXml(signedXml);
            var x509Node = xmlDoc.GetRoot().GetChildWithTag("Signature")?
                .GetChildWithTag("KeyInfo")?
                .GetChildWithTag("X509Data");
            var x509Serial = xmlDoc.NewChild("X509IssuerSerial", String.Empty);
            var x509Name = xmlDoc.NewChild("X509IssuerName", issuerName);
            var x509Number = xmlDoc.NewChild("X509SerialNumber", serialNumber);
            x509Serial.AddChildTree(x509Name);
            x509Serial.AddChildTree(x509Number);
            x509Node.AddChildTree(x509Serial);
            return XmlHelper.RemoveInvalidCharsXml(xmlDoc.GetXml());
        }
        catch (XmlException ex)
        {
            throw new Exception($"El XML no es válido.{ex.Message}");
        }
        catch (CryptographicException ex)
        {
            throw new Exception($"El certificado no es un PFX válido.{ex.Message}");
        }
    }
   
}
