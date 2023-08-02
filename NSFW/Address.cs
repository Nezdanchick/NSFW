using System.Net;
using System.Net.Sockets;

namespace NSFW
{
    /// <summary>
    /// Сlass for working with addresses
    /// </summary>
    public static class Address
    {
        /// <summary>
        /// Default port for library
        /// </summary>
        public const int DefaultPort = 12345;
        /// <summary>
        /// Get local IP address
        /// </summary>
        public static IPAddress LocalIP { get => GetLocal(); }
        /// <summary>
        /// Get local IP address with Default port
        /// </summary>
        public static IPEndPoint DefaultEndPoint { get => new(LocalIP, DefaultPort); }

        /// <summary>
        /// Get local IP address
        /// </summary>
        /// <returns>Local IP</returns>
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
        /// Get address from end point
        /// </summary>
        /// <param name="endPoint">End point with address</param>
        /// <returns>IP address</returns>
        public static IPAddress GetAddress(EndPoint? endPoint)
        {
            var point = (IPEndPoint?)endPoint;
            return point?.Address ?? LocalIP;
        }
        /// <summary>
        /// Get port from end point
        /// </summary>
        /// <param name="endPoint">End point with port</param>
        /// <returns>Port</returns>
        public static int GetPort(EndPoint? endPoint)
        {
            var point = (IPEndPoint?)endPoint;
            return point?.Port ?? DefaultPort;
        }
    }
}
