using System;
using System.Net;
using System.Net.Sockets;

namespace NSFW
{
    public static class Address
    {
        public const int DefaultPort = 8888;
        public static IPAddress LocalIP { get => GetLocal(); }
        public static IPEndPoint DefaultEndPoint { get => new(LocalIP, DefaultPort); }

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
        /// Get address from end point
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static IPAddress GetAddress(EndPoint? endPoint)
        {
            var point = (IPEndPoint?)endPoint;
            return point?.Address ?? LocalIP;
        }
        /// <summary>
        /// Get port from end point
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static int GetPort(EndPoint? endPoint)
        {
            var point = (IPEndPoint?)endPoint;
            return point?.Port ?? DefaultPort;
        }
    }
}
