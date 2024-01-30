using SW.Services.Stamp;
using SW.Tools.Services.Sign;
using SW.Tools.UnitTest.Helpers;

namespace SW.Tools.UnitTest
{
    public class SignTest
    {
        private readonly string _password;
        private readonly Stamp _stamp;
        private readonly Sign _sign;
        public SignTest()
        {
            _password = "12345678a";
            _stamp = new(BuildTest.UrlService, BuildTest.Token);
            _sign = new();
        }
        [Fact]
        public void Sign_Cfdi40_Success()
        {
            var result = _sign.SignCfdi(ResourceHelper.GetInvoice("cfdi40.xml"),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            CustomAssert.StampIsSuccess(_stamp, result.Data.Xml);
        }       
        [Fact]
        public void Sign_Cfdi40WithComplement_Success()
        {
            var result = _sign.SignCfdi(ResourceHelper.GetInvoice("cp30.xml"),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            CustomAssert.StampIsSuccess(_stamp, result.Data.Xml);
        }
        [Fact]
        public void Sign_Retention20_Success()
        {
            var result = _sign.SignCfdi(ResourceHelper.GetInvoice("cp30.xml"),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
        }
        [Fact]
        public void Sign_Retention20WithComplement_Success()
        {
            var result = _sign.SignRetention(ResourceHelper.GetInvoice("retention20.xml"),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
        }
        [Fact]
        public void Sign_Cfdi40_InvalidInvoice_Error()
        {
            var result = _sign.SignCfdi("<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
           CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void Sign_Retention20_InvalidInvoice_Error()
        {
            var result = _sign.SignRetention("<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void Sign_InvalidCert_Error()
        {
            var result = _sign.SignCfdi(ResourceHelper.GetInvoice("cfdi40.xml"),
                ResourceHelper.GetCertResource("csd_h&e951128469.cer"), _password);
            CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void Sign_InvalidPassword_Error()
        {
            var result = _sign.SignCfdi(ResourceHelper.GetInvoice("cfdi40.xml"),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), "password");
            CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void Sign_Cfdi40WithComplementCCE20_Success()
        {
            var result = _sign.SignCfdi(ResourceHelper.GetInvoice("cce20.xml"),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            CustomAssert.StampIsSuccess(_stamp, result.Data.Xml);
        }
    }
}