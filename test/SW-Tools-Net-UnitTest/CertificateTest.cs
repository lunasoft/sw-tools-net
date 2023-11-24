using SW.Services.Stamp;
using SW.Tools.UnitTest.Helpers;
using SW.Tools.Services.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace SW.Tools.UnitTest
{
    public class CertificateTest
    {
        private readonly byte[] _BytesCer;
        private readonly byte[] _BytesKey;
        private readonly string _password;

        public CertificateTest()
        {
            _BytesCer = BuildTest.publicCertificate;
            _BytesKey = BuildTest.privateKey;
            _password = BuildTest.passwordCer;
        }
        [Fact]
        public void CreatePfx_Success()
        {
            Certificate certificate = new();
            var result = certificate.CreatePfx(_BytesCer, _BytesKey, _password);
            Assert.True(result.Data.Pfx !=null);
            Assert.True(result.Status == "success");
            var pfx = result.Data.Pfx;
            X509Certificate x509 = new X509Certificate(pfx, _password);
            Assert.True(x509.GetPublicKey() != null);
        }
        [Fact]
        public void CreatePfx_InvalidPassword_Error()
        {
            Certificate certificate = new();
            var result = certificate.CreatePfx(_BytesCer, _BytesKey, "12345");
            var message = "Los datos del Certificado CER KEY o Password son incorrectos. No es posible leer la llave privada.";
            Assert.True(result.Message == message);
            Assert.True(result.Status == "error");
        }
        [Fact]
        public void CreatePfx_CsdNull_Error()
        {
            Certificate certificate = new();
            var result = certificate.CreatePfx(null, _BytesKey, _password);
            var message = "Los datos del Certificado CER KEY o Password son incorrectos. No es posible leer la llave privada.";
            Assert.True(result.Message == message);
            Assert.True(result.Status == "error");
        }
    }
}
