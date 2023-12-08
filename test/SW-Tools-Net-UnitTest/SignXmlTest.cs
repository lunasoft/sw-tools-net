using SW.Services.AcceptReject;
using SW.Services.Cancelation;
using SW.Services.Stamp;
using SW.Tools.Services.Sign;
using SW.Tools.UnitTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SW.Tools.UnitTest
{
    public class SignXmlTest
    {
        private readonly string _password;
        public SignXmlTest()
        {
            _password = "12345678a";
        }
        [Fact]
        public void Sign_Cancelattion_Succes()
        {
            Sign sign = new();
            CustomAssert asserts = new CustomAssert();
            var result = sign.SignXml(ResourceHelper.GetInvoice("cancelation_Signed.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml, true);
        }
        [Fact]
        public void SignedXml_Cancelattion_Succes()
        {
            Sign sign = new();
            CustomAssert asserts = new CustomAssert();
            var result = sign.SignXml(ResourceHelper.GetInvoice("cancelation_Signed.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml, true);
        }
        [Fact]
        public void Sign_CancelattionWithSignatureNode_Succes()
        {
            Sign sign = new();
            CustomAssert asserts = new CustomAssert();
            var result = sign.SignXml(ResourceHelper.GetInvoice("cancelation_Signed.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml, true);
        }
        [Fact]
        public void Sign_AceptedRejected_Success()
        {
            Sign sign = new();
            CustomAssert asserts = new CustomAssert();
            var result = sign.SignXml(ResourceHelper.GetInvoice("accept.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml);
        }
        [Fact]
        public void SignedXml_AceptedRejected_Success()
        {
            Sign sign = new();
            CustomAssert asserts = new CustomAssert();
            var result = sign.SignXml(ResourceHelper.GetInvoice("accept_Signed.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml);
        }
        [Fact]
        public void SignedXml_AceptedRejectedWithSignatureNode_Success()
        {
            Sign sign = new();
            CustomAssert asserts = new CustomAssert();
            var result = sign.SignXml(ResourceHelper.GetInvoice("accept_nodeSign.xml", true),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsSuccess(result);
            Assert.True(!String.IsNullOrEmpty(result.Data.Xml));
            asserts.CancellationAcceptRejectIsSuccess(result.Data.Xml);
        }
        [Fact]
        public void Sign_InvalidXml_Error()
        {
            Sign sign = new();
            var result = sign.SignXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), _password);
            CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void Sign_EmptyXml_Error()
        {
            Sign sign = new();
            var result = sign.SignCfdi(String.Empty,
                ResourceHelper.GetCertResource("csd_h&e951128469.cer"), _password);
            CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void SignXml_InvalidPfx_Error()
        {
            Sign sign = new();
            var result = sign.SignCfdi(ResourceHelper.GetInvoice("cfdi40.xml"),
                ResourceHelper.GetCertResource("csd_h&e951128469.cer"), _password);
            CustomAssert.ResultIsError(result);
        }
        [Fact]
        public void SignXml_InvalidPassword_Error()
        {
            Sign sign = new();
            var result = sign.SignCfdi(ResourceHelper.GetInvoice("cancelation_Signed.xml"),
                ResourceHelper.GetCertResource("pfx_h&e951128469.pfx"), "passwor");
            CustomAssert.ResultIsError(result);
        }
    }
}
