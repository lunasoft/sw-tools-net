using System.Text.RegularExpressions;
using System.Xml;

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
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNodeList signatureNodes = xmlDoc.GetElementsByTagName("Signature");

            if (signatureNodes != null)
            {
                int index = 0;
                while (index < signatureNodes.Count)
                {
                    signatureNodes[index].ParentNode.RemoveChild(signatureNodes[index]);
                }
                return RemoveInvalidCharsXml(xmlDoc.OuterXml);
            }
            else
            {
                return xml;
            }
            
        }
    }
}
