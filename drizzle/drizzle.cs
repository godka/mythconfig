using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

using System.Threading;
//KA 2013年4月1日15:45:10
namespace drizzle
{
    public class drizzleTCP
    {
        /// <summary>
        /// 服务器
        /// </summary>
        public class Server
        {
            private IPAddress ipAddr;
            private TcpListener Listener;
            private Thread ListenThread;
            //private int Uid = 0;
            private static List<Tcpreceiver> ListTcp; //= new List<TcpClient>();
            private bool isKeepListen = true;
            public delegate void ServerDelegateArrive(Server.Tcpreceiver tcp, string Receive);
            public event ServerDelegateArrive ServerEventArrive;
            public delegate void ServerDelegateArriveByte(Server.Tcpreceiver tcp, byte[] Data);
            public event ServerDelegateArriveByte ServerEventArriveByte;
            public int connect(string host, int port)
            {
                try
                {
                    //IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                    //IPAddress ipa = ipe.AddressList[0];

                    ipAddr = IPAddress.Parse(host);
                    Listener = new TcpListener(ipAddr, port);
                    Listener.Start();
                    ListenThread = new Thread(keeplisten);
                    ListenThread.Start();
                    ListTcp = new List<Tcpreceiver>();
                    //Console.WriteLine(ipa.ToString());
                    return Listener.GetHashCode();
                }
                catch (Exception ee)
                {
                    Console.WriteLine("连接失败，请查看端口是否被占用");
                    Console.WriteLine(ee.Message.ToString());
                    return -1;
                }
            }
            public Server(string host, int port)
            {
                connect(host, port);
            }
            public Server(int port)
            {
                connect("0.0.0.0", port);
            }
            public int disconnect()
            {
                try
                {
                    foreach (Tcpreceiver tcp in ListTcp)
                    {
                        try
                        {
                            tcp.stop();
                        }
                        catch { }
                    }
                    ListenThread.Abort();
                    Listener.Stop();
                    return Listener.GetHashCode();
                }
                catch
                {
                    return -1;
                }
            }
            public void sendmessage(string str)
            {
                foreach (Tcpreceiver tcp in ListTcp)
                {
                    try
                    {
                        tcp.sendmessage(str);
                    }
                    catch { }
                }
            }
            /// <summary>
            /// 监听子程
            /// </summary>
            public void keeplisten()
            {
                TcpClient tcpClient;
                //开启死循环
                while (isKeepListen)
                {
                    tcpClient = Listener.AcceptTcpClient();
                    Tcpreceiver tcprece = new Tcpreceiver(tcpClient);
                    tcprece.TcpreceiverEventArrive += new Tcpreceiver.TcpreceiverDelegateArrive(tcprece_TcpreceiverEventArrive);
                    tcprece.TcpreceiverEventArriveByte += new Tcpreceiver.TcpreceiverDelegateArriveByte(tcprece_TcpreceiverEventArriveByte);
                    ListTcp.Add(tcprece);

                }
                foreach (Tcpreceiver tcp in ListTcp)
                {
                    tcp.stop();
                }
            }

            void tcprece_TcpreceiverEventArriveByte(Server.Tcpreceiver Uid, byte[] data)
            {
                if(ServerEventArriveByte!=null)
                    ServerEventArriveByte(Uid, data);
                //throw new NotImplementedException();
            }

            private void tcprece_TcpreceiverEventArrive(Server.Tcpreceiver Uid, string Receive)
            {
                if (ServerEventArrive != null)
                    ServerEventArrive(Uid, Receive);
                //throw new NotImplementedException();
            }
            public class Tcpreceiver
            {
                private TcpClient tcpClient;
                private NetworkStream ns = null;
                private bool isrunning = true;
                private Thread receicethread;
                private int Uid = -1;
                public string IP
                {
                    get
                    {
                        return ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
                    }
                }
                public delegate void TcpreceiverDelegateArrive(Tcpreceiver Uid, string Receive);
                public delegate void TcpreceiverDelegateArriveByte(Tcpreceiver Uid, byte[] data);
                public event TcpreceiverDelegateArrive TcpreceiverEventArrive;
                public event TcpreceiverDelegateArriveByte TcpreceiverEventArriveByte;
                public Tcpreceiver(TcpClient tcp)
                {
                    tcpClient = tcp;
                    receicethread = new Thread(receive);
                    receicethread.Start();
                }
                public void stop()
                {
                    isrunning = false;
                    tcpClient.Close();
                    //receicethread.Abort();
                }
                private void receive()
                {
                    string str;
                    while (isrunning)
                    {

                        try
                        {
                            byte[] data = new byte[255];
                            ns = tcpClient.GetStream();
                            int stringLENGTH = ns.Read(data, 0, 255);
                            if (stringLENGTH > 0)
                            {
                                str = Encoding.UTF8.GetString(data, 0, stringLENGTH);
                                ///////////////////////////////////////////////////////////
                                //do events
                                //////////////////////////////////////////////////////////
                                if (TcpreceiverEventArrive != null)
                                {
                                    
                                    TcpreceiverEventArrive(this, str);
                                }
                                if (TcpreceiverEventArriveByte != null)
                                {
                                    TcpreceiverEventArriveByte(this, data);
                                }
                                GC.Collect();
                            }
                            Application.DoEvents();
                        }
                        catch (Exception ee)
                        {
                            isrunning = false;
                            tcpClient.Close();
                        }
                    }
                }
                public void sendmessage(string str)
                {
                    byte[] tmpbyte = Encoding.Default.GetBytes(str);
                    ns.Write(tmpbyte, 0, tmpbyte.Length);
                    ns.Flush();
                    Console.WriteLine(((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port.ToString() + ":" + str);
                }
            }
            private void console_write(string ConsoleString)
            {
                Console.WriteLine(ConsoleString);
            }

        }
        /// <summary>
        /// 客户端
        /// </summary>
        public class Client
        {
            TcpClient tcp = null;
            private bool isrunning = false;
            private Thread THrecevie;
            private NetworkStream ns;
            private string Clientip;
            private int Clientport;
            public delegate void ClientDelegateArrive(TcpClient tcp, string Receive);
            public delegate void ClientDelegateArriveByte(TcpClient tcp, byte[] Receive,int length);
            public event ClientDelegateArrive ClientEventArrive;
            public event ClientDelegateArriveByte ClientEventArriveByte;
            public Client(string IP, int port)
            {
                Clientip = IP;
                Clientport = port;
                connect(IP, port);
            }
            public void connect(string IP, int port,bool autostart = false)
            {
                try
                {
                    tcp = new TcpClient();
                    //tcp.ReceiveTimeout = 1000;
                    tcp.Connect(IP, port);
                    ns = tcp.GetStream();
                    isrunning = true;
                    if (autostart)
                    {
                        THrecevie = new Thread(receive);
                        THrecevie.Start();
                    }
                }
                catch(Exception ee)
                {
                    //Thread.Sleep(1000);
                    tcp.Close();
                    //重连接
                    Console.WriteLine("reconnect");
                    //connect(Clientip, Clientport);
                }
            }
            public void sendmessage(string str)
            {
                byte[] tmpbyte = Encoding.UTF8.GetBytes(str);
                ns.Write(tmpbyte, 0, tmpbyte.Length);
                ns.Flush();
            }

            public string sendmessageNoAsync(string str)
            {
                if (ns == null) return string.Empty;
                byte[] data = new byte[1024*1024];
                byte[] tmpbyte = Encoding.Default.GetBytes(str);
                ns.Write(tmpbyte, 0, tmpbyte.Length);
                ns.Flush();
                int stringLENGTH = 0;
                StringBuilder s = new StringBuilder();
                for (; ; )
                {
                    stringLENGTH = ns.Read(data, 0, 1024 * 1024);
                    ns.Flush();
                    if (stringLENGTH > 0)
                    {
                        string tmpstr = Encoding.Default.GetString(data, 0, stringLENGTH);
                        s.Append(tmpstr);
                    }
                    else
                    {
                        return s.ToString();
                    }
                    //ClientEventArrive(tcp, str);
                }
                return string.Empty;
            }

            public void send(byte[] tmpbyte)
            {
                //byte[] tmpbyte = Encoding.UTF8.GetBytes(str);
                ns.Write(tmpbyte, 0, tmpbyte.Length);
                ns.Flush();
            }
            public void close()
            {
                this.tcp.Close();
            }
            private void receive()
            {
                try
                {
                    string str = string.Empty;
                    byte[] data = new byte[4096];
                    while (isrunning)
                    {
                        int stringLENGTH = ns.Read(data, 0, 4096);
                        ns.Flush();
                        if (stringLENGTH > 0)
                        {
                            if (ClientEventArrive != null)
                            {
                                str = Encoding.UTF8.GetString(data, 0, stringLENGTH);
                                ClientEventArrive(tcp, str);
                            }
                            if (ClientEventArriveByte != null)
                            {
                                ClientEventArriveByte(tcp, data, stringLENGTH);
                            }
                        }
                        Application.DoEvents();

                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                    tcp.Close();
                    //重连接
                    connect(Clientip, Clientport);
                    //tcp.Connect(Clientip, Clientport);
                    isrunning = false;
                }
            }
        }
    }
}
