using System.Net;
using System.Net.Sockets;

namespace NSFW
{
    internal static class Address
    {
        public static IPAddress Default { get => IPAddress.Loopback; }

        public static IPAddress GetGlobal()
        {
            string ip = new System.Net.Http.HttpClient().GetStringAsync("https://api.ipify.org").Result;
            IPAddress? address = IPAddress.Parse(ip);
            return address ?? Default;
        }
        public static IPAddress GetLocal()
        {
            IPAddress address;
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint? endPoint = (IPEndPoint?)socket.LocalEndPoint;
                address = endPoint?.Address ?? Default;
            }
            return address;
        }
    }
}
