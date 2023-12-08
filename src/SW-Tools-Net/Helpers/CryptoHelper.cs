using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using SW.Tools.Transforms;
using Chilkat;

namespace SW.Tools.Helpers;

internal class CryptoHelper
{
    internal static string GetHashedStringSha256(string originalChain, X509Certificate2 certificate)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] dataBytes = Encoding.UTF8.GetBytes(originalChain);
        byte[] hashedBytes = sha256.ComputeHash(dataBytes);
        using RSA rsa = certificate.GetRSAPrivateKey();
        byte[] signature = rsa != null ? rsa.SignHash(hashedBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)
            : throw new CryptographicException("El certificado no contiene una llave privada.");
        return Convert.ToBase64String(signature);
    }
    internal static string GetOriginalString(string xml, bool isRetention)
    {
        return isRetention ? new XslRetentionTransform(xml).Transform() : new XslCfdiTransform(xml).Transform();
    }
    internal static (string b64Certificate, string certificateNumber) GetCertificateValues(X509Certificate2 certificate)
    {
        string hexadecimalString = certificate.SerialNumber;
        System.Text.StringBuilder sb = new();
        for (int i = 0; i <= hexadecimalString.Length - 2; i += 2)
        {
            sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexadecimalString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
        }
        return (Convert.ToBase64String(certificate.GetRawCertData()), sb.ToString());
    }
    internal static (XmlDSigGen signature, string issuerName, string serialNumber) GetSignatureSha1(Chilkat.Pfx pfx, string location)
    {
        new Global().UnlockBundle("LUNASF.CB1122019_6AkG5xXC37m0");
        XmlDSigGen gen = new();
        Cert cert = pfx.GetCert(0);
        gen.SigLocation = location;
        gen.SigNamespacePrefix = "";
        gen.SignedInfoCanonAlg = "C14N";
        gen.SignedInfoDigestMethod = "sha1";
        gen.AddSameDocRef("", "sha1", "", "", "");
        gen.SetX509Cert(cert, true);
        gen.KeyInfoType = "X509Data";
        gen.X509Type = "Certificate";
        gen.Behaviors = "ForceAddEnvelopedSignatureTransform";
        gen.Behaviors.Trim();
        return (gen, cert.IssuerDN, cert.SerialNumber);
    }
}
