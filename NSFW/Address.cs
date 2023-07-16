using System.Net;
using System.Net.Sockets;

namespace NSFW
{
    internal static class Address
    {
        public static IPAddress DefaultIP { get => GetLocal(); }
        public const int DefaultPort = 0;

        public static IPAddress GetGlobal()
        {
            string ip = new HttpClient().GetStringAsync("https://api.ipify.org").Result;
            IPAddress? address = IPAddress.Parse(ip);
            return address ?? DefaultIP;
        }
        public static IPAddress GetLocal()
        {
            IPAddress address;
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", DefaultPort);
                IPEndPoint? endPoint = (IPEndPoint?)socket.LocalEndPoint;
                address = endPoint?.Address ?? DefaultIP;
            }
            return address;
        }
    }
}
