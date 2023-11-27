using SW.Services.Stamp;
using SW.Tools.UnitTest.Helpers;
using SW.Tools.Services.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;

namespace SW.Tools.UnitTest
{
    public class CertificateTest
    {
        private readonly byte[] _bytesCer;
        private readonly byte[] _bytesKey;
        private readonly string _password;

        public CertificateTest()
        {
            _bytesCer = BuildTest.publicCertificate;
            _bytesKey = BuildTest.privateKey;
            _password = BuildTest.passwordCer;
        }
        [Fact]
        public void CreatePfx_Success()
        {
            Certificate certificate = new();
            var result = certificate.CreatePfx(_bytesCer, _bytesKey, _password);
            Assert.True(result.Data.Pfx !=null);
            Assert.True(result.Status == "success");
            CustomAssert.ResultIsSuccess(result);
            var pfx = result.Data.Pfx;
            X509Certificate x509 = new X509Certificate(pfx, _password);
            Assert.True(x509.GetPublicKey() != null);
        }
        [Fact]
        public void CreatePfx_InvalidPassword_Error()
        {
            Certificate certificate = new();
            var result = certificate.CreatePfx(_bytesCer, _bytesKey, "12345");
            var message = "La llave privada es incorrecta o la contraseña es invalida.";
            CustomAssert.ResultIsError(result,message);
        }
        [Fact]
        public void CreatePfx_CsdNull_Error()
        {
            Certificate certificate = new();
            var result = certificate.CreatePfx(null, _bytesKey, _password);
            var message = "Los parámetros Certificado, llave privada y contraseña son requeridos.";
            CustomAssert.ResultIsError(result, message);
        }
        [Fact]
        public void CreatePfx_KeyNull_Error()
        {
            Certificate certificate = new();
            var result = certificate.CreatePfx(_bytesCer, null, _password);
            var message = "Los parámetros Certificado, llave privada y contraseña son requeridos.";
            CustomAssert.ResultIsError(result, message);
        }
        [Fact]
        public void CreatePfx_InvalidCSD_Error()
        {
            Certificate certificate = new();
            var result = certificate.CreatePfx(_bytesKey, _bytesKey, _password);
            var message = "El certificado debe ser de extensión cer o crt.";
            CustomAssert.ResultIsError(result, message);
        }
        [Fact]
        public void CreatePfx_InvalidKey_Error()
        {
            Certificate certificate = new();
            var result = certificate.CreatePfx(_bytesCer, _bytesCer, _password);
            var message = "La llave privada es incorrecta o la contraseña es invalida.";
            CustomAssert.ResultIsError(result, message);
        }
    }
}
