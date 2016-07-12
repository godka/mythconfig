using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace drizzle
{
    public class drizzleXML
    {
        private string mip;
        private int mport;
        private drizzleTCP.Client client;
        public drizzleXML(string ip, int port)
        {
            mip = ip;
            mport = port;
        }
        public void connect()
        {
            client = new drizzleTCP.Client(mip, mport);
            //client.ClientEventArrive += client_ClientEventArrive;
        }

        public drizzleXMLReader SendRequest(string sqlrequest)
        {
            connect();
            //string tmp = sqlrequest.Replace(" ", "%20");
            string str = "GET /scripts/dbnet.dll?param=<XML><function>SQL_SELECT</function><Content>";
            string decodeSQL = sqlrequest.ToUpper().Replace(" ", "%20");
            string setstr = str + decodeSQL + "</Content></XML>  HTTP/1.0\r\n\r\n";
            string retstr = client.sendmessageNoAsync(setstr);
            return new drizzleXMLReader(retstr);
            
        }
    }
}
