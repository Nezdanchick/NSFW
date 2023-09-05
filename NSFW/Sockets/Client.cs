using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NSFW.Sockets
{
    /// <summary>
    /// Wrapper over a socket that provides client capabilities
    /// </summary>
    public class Client : TcpSocket
    {
        /// <summary>
        /// Create new client
        /// </summary>
        public Client() : base() { }
        /// <summary>
        /// Create new client
        /// </summary>
        /// <param name="socket">Socket to create a client</param>
        public Client(Socket socket) : base(socket) { }

        /// <summary>
        /// Connect client to server
        /// </summary>
        /// <param name="endPoint">Connection address. For example 192.168.0.1:12345</param>
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
        }
        /// <summary>
        /// Send data to server
        /// </summary>
        /// <param name="data">Data to send</param>
        public override void Send(byte[]? data)
        {
            if (data != null)
                DataExchange.Send(Socket, data);
        }
        /// <summary>
        /// Receive data from server
        /// </summary>
        /// <returns>Received data</returns>
        public override byte[]? Receive() =>
            DataExchange.Receive(Socket);
    }
}