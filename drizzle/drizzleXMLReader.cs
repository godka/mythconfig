using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace drizzle
{
    public class drizzleXMLReader
    {
        public bool success;
        XmlDocument xd;
        XmlElement xe;
        XmlLinkedNode tablecontent = null;
        XmlNode lines = null;
        public string[] GetTableSchema()
        {
            string tmp = string.Empty;
            List<string> mlist = new List<string>();
            var xheader = xe["TableSchema"];
            if (xheader != null)
            {
                tmp = xheader.FirstChild.InnerText.ToString().Replace("\r\n","");
            }
            return tmp.Split(',');
        }
        public drizzleXMLReader(string str)
        {
            if (str == string.Empty)
            {
                success = false;
                return;
            }
            try
            {
                str = str.Replace("&", "&amp;");
                xd = new XmlDocument();
                xd.LoadXml(str);
                xe = xd.DocumentElement;
                tablecontent = xe["TableContent"];
                if (tablecontent != null)
                {
                    lines = tablecontent.FirstChild;
                    if (lines == null)
                    {
                        success = false;
                    }else
                        success = true;
                }
                else
                {
                    success = false;
                }

            }
            catch (Exception ee)
            {
                success = false;
            }
            //var tmp = xd.GetElementsByTagName
        }
        public bool moveNext()
        {
            if (lines.NextSibling != null)
            {
               lines = lines.NextSibling;
                return true;
            }
            else
            {
                return false;
            }
        }
        public string Parse(string key)
        {
            //key = key.ToUpper();
            if (lines != null)
            {
                var tmp = lines[key];
                if (tmp != null)
                {
                    return tmp.InnerText.ToString();
                }
                else
                {
                    tmp = lines[key.ToUpper()];
                    if (tmp != null)
                    {
                        return tmp.InnerText.ToString();
                    }
                    else
                        return string.Empty;
                }
            }
            else
                return string.Empty;
        }
    }
}
