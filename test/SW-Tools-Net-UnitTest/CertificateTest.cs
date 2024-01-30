using SW.Tools.UnitTest.Helpers;
using SW.Tools.Services.Certificate;
using System.Security.Cryptography.X509Certificates;

namespace SW.Tools.UnitTest
{
    public class CertificateTest
    {
        private readonly byte[] _bytesCer;
        private readonly byte[] _bytesKey;
        private readonly byte[] _bytesPfx;
        private readonly string _password;
        private readonly Certificate _certificate;

        public CertificateTest()
        {
            _bytesCer = BuildTest.publicCertificate;
            _bytesKey = BuildTest.privateKey;
            _bytesPfx = BuildTest.pfx;
            _password = BuildTest.passwordCer;
            _certificate = new();
        }
        [Fact]
        public void CreatePfx_Success()
        {
            var result = _certificate.CreatePfx(_bytesCer, _bytesKey, _password);
            Assert.True(result.Data.Pfx !=null);
            Assert.True(result.Status == "success");
            CustomAssert.ResultIsSuccess(result);
            var pfx = result.Data.Pfx;
            X509Certificate x509 = new(pfx, _password);
            Assert.True(x509.GetPublicKey() != null);
        }
        [Fact]
        public void CreatePfx_InvalidPassword_Error()
        {
            var result = _certificate.CreatePfx(_bytesCer, _bytesKey, "12345");
            var message = "La llave privada es incorrecta o la contraseña es invalida.";
            CustomAssert.ResultIsError(result,message);
        }
        [Fact]
        public void CreatePfx_CsdNull_Error()
        {
            var result = _certificate.CreatePfx(null, _bytesKey, _password);
            var message = "Los parámetros Certificado, llave privada y contraseña son requeridos.";
            CustomAssert.ResultIsError(result, message);
        }
        [Fact]
        public void CreatePfx_KeyNull_Error()
        {
            var result = _certificate.CreatePfx(_bytesCer, null, _password);
            var message = "Los parámetros Certificado, llave privada y contraseña son requeridos.";
            CustomAssert.ResultIsError(result, message);
        }
        [Fact]
        public void CreatePfx_InvalidCSD_Error()
        {
            var result = _certificate.CreatePfx(_bytesKey, _bytesKey, _password);
            var message = "El certificado debe ser de extensión cer o crt.";
            CustomAssert.ResultIsError(result, message);
        }
        [Fact]
        public void CreatePfx_InvalidKey_Error()
        {
            var result = _certificate.CreatePfx(_bytesCer, _bytesCer, _password);
            var message = "La llave privada es incorrecta o la contraseña es invalida.";
            CustomAssert.ResultIsError(result, message);
        }
        [Fact]
        public void ReadPfx_Success()
        {
            var result = _certificate.ReadPfx(_bytesPfx, _password);
            var publicCertificate = result.Data.PublicCert;
            var privateCert = result.Data.PrivateCert;
            Assert.True(publicCertificate != null);
            Assert.True(privateCert != null);
            Assert.True(result.Status == "success");
            CustomAssert.ResultIsSuccess(result);
            X509Certificate x509 = new(publicCertificate);
            Assert.True(x509.GetPublicKey() != null);
            x509 = new(privateCert, _password);
            Assert.True(x509.GetPublicKey() != null);
        }
        [Fact]
        public void ReadPfx_InvalidPassword_Error()
        {
            var result = _certificate.ReadPfx(_bytesCer, "12345");
            var message = "El archivo PFX es incorrecto o la contraseña es invalida.";
            CustomAssert.ResultIsError(result, message);
        }
        [Fact]
        public void ReadPfx_PfxNull_Error()
        {
            var result = _certificate.ReadPfx(null, _password);
            var message = "Los parámetros PFX y contraseña son requeridos.";
            CustomAssert.ResultIsError(result, message);
        }
    }
}
