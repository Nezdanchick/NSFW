using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace NSFW.Sockets
{
    public class Client : TcpSocket
    {
        public string? Name { get; private set; }

        public Client() : base() { }
        public Client(Socket socket) : base(socket) { }

        public override event Action OnConnect = () => { };

        public void Connect(string? endPoint)
        {
            try
            {
                var address = IPEndPoint.Parse(endPoint ?? "");
                Socket?.Connect(address);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(2000);
                Environment.Exit(-1);
            }
            OnConnect();
        }
    }
}