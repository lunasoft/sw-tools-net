using SW.Tools.Services.Sign;
using SW.Tools.UnitTest.Helpers;

namespace SW.Tools.UnitTest
{
    public class SignXmlTest
    {
        private readonly string _password;
        private readonly Sign _sign;
        private readonly CustomAssert _asserts;
        public SignXmlTest()
        {
            _password = "12345678a";
            _sign = new();
            _asserts = new();
        }
        [Fact]
        public void Sign_Cancellation_Succes()
        {
            var result = _sign.SignXml(ResourceHelper.GetInvoice("cancelation.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            _asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml, true);
        }
        [Fact]
        public void SignedXml_Cancellation_Succes()
        {
            var result = _sign.SignXml(ResourceHelper.GetInvoice("cancelation_Signed.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            _asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml, true);
        }
        [Fact]
        public void Sign_CancellationWithSignatureNode_Succes()
        {
            var result = _sign.SignXml(ResourceHelper.GetInvoice("cancelation_Signed.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            _asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml, true);
        }
        [Fact]
        public void Sign_AceptedRejected_Success()
        {
            var result = _sign.SignXml(ResourceHelper.GetInvoice("accept.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            _asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml);
        }
        [Fact]
        public void SignedXml_AceptedRejected_Success()
        {
            var result = _sign.SignXml(ResourceHelper.GetInvoice("accept_Signed.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            _asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml);
        }
        [Fact]
        public void SignedXml_AceptedRejectedWithSignatureNode_Success()
        {
            var result = _sign.SignXml(ResourceHelper.GetInvoice("accept_nodeSign.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            _asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml);
        }
        [Fact]
        public void Sign_InvalidXml_Error()
        {
            var result = _sign.SignXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void Sign_EmptyXml_Error()
        {
            var result = _sign.SignCfdi(String.Empty,
                ResourceHelper.GetCertResource("csd_h&e951128469.cer"), _password);
            CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void SignXml_InvalidPfx_Error()
        {
            var result = _sign.SignCfdi(ResourceHelper.GetInvoice("cfdi40.xml"),
                ResourceHelper.GetCertResource("csd_h&e951128469.cer"), _password);
            CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void SignXml_InvalidPassword_Error()
        {
            var result = _sign.SignCfdi(ResourceHelper.GetInvoice("cancelation_Signed.xml"),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), "passwor");
            CustomAssert.ResultIsError(result);
        }
    }
}
