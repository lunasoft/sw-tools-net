using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SW.Tools.Helpers
{
    internal class XmlHelper
    {
        internal static string RemoveInvalidCharsXml(string str)
        {
            str = str.Replace("\r\n", "");
            str = str.Replace("\r", "");
            str = str.Replace("\n", "");
            str = str.Replace(@"<?xml version=""1.0"" encoding=""utf-16""?>", @"<?xml version=""1.0"" encoding=""utf-8""?>").Trim();
            str = str.Replace("﻿", "");
            str = str.Replace(@"
", "");
            str = Regex.Replace(str, @"\s+", " ");
            str = str.Replace("> ", ">");
            str = str.Replace(" />", "/>");
            return str;
        }
        internal static string RemoveSignatureNodes(string xml)
        {
            Chilkat.Xml xmlDoc = new Chilkat.Xml();
            xmlDoc.LoadXml(xml);
            var signatureNode = xmlDoc.SearchForTag(xmlDoc, "Signature");
            if (signatureNode != null)
            {
                while (signatureNode != null)
                {
                    signatureNode.RemoveFromTree();
                    signatureNode = xmlDoc.SearchForTag(xmlDoc, "Signature");
                }
                return RemoveInvalidCharsXml(xmlDoc.GetXml());
            }
            else
            {
                return xml;
            }
        }
    }
}
