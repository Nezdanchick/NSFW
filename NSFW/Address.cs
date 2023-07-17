using System.Net;
using System.Net.Sockets;

namespace NSFW
{
    internal static class Address
    {
        public static IPAddress LocalIP { get => GetLocal(); }
        public const int DefaultPort = 0;

        /// <summary>
        /// Get local ip address
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocal()
        {
            IPAddress address;
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", DefaultPort);
                IPEndPoint? endPoint = (IPEndPoint?)socket.LocalEndPoint;
                address = endPoint?.Address ?? LocalIP;
            }
            return address;
        }
        /// <summary>
        /// Get port from end point
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static IPAddress GetAddress(EndPoint? endPoint)
        {
            var point = endPoint as IPEndPoint;
            var address = point?.Address;
            return address ?? LocalIP;
        }
        /// <summary>
        /// Get port from end point
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static int GetPort(EndPoint? endPoint)
        {
            var point = endPoint as IPEndPoint;
            var port = point?.Port;
            return port ?? DefaultPort;
        }
    }
}
