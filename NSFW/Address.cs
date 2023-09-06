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
        /// Get local IP address
        /// </summary>
        public static IPAddress LocalIP { get => GetLocal(); }

        /// <summary>
        /// Get local IP address
        /// </summary>
        /// <returns>Local IP</returns>
        public static IPAddress GetLocal()
        {
            IPAddress address;
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 0);
                IPEndPoint? endPoint = (IPEndPoint?)socket.LocalEndPoint;
                address = endPoint?.Address ?? LocalIP;
            }
            return address;
        }
        /// <summary>
        /// Get end point with local ip and specified port
        /// </summary>
        /// <returns>Local IP with Port</returns>
        public static IPEndPoint GetLocalEndPoint(int port) =>
            new(GetLocal(), port);
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
        public static IPAddress GetAddress(string endPoint)
        {
            var success = IPEndPoint.TryParse(endPoint, out var point);
            return success ? GetAddress(point) : LocalIP;
        }
        /// <summary>
        /// Get port from end point
        /// </summary>
        /// <param name="endPoint">End point with port</param>
        /// <returns>Port</returns>
        public static int GetPort(EndPoint? endPoint)
        {
            var point = (IPEndPoint?)endPoint;
            return point?.Port ?? 0;
        }
        /// <summary>
        /// Get port from end point
        /// </summary>
        /// <param name="endPoint">End point with port</param>
        /// <returns>Port</returns>
        public static int GetPort(string endPoint)
        {
            var success = IPEndPoint.TryParse(endPoint, out var point);
            return success ? GetPort(point) : 0;
        }
        /// <summary>
        /// Is ip address valid
        /// </summary>
        /// <param name="address">String represents ip endPoint</param>
        /// <returns></returns>
        public static bool IsValid(string address) =>
            address.Contains(':') && address.Contains('.') &&
                IPEndPoint.TryParse(address, out var _);
    }
}
