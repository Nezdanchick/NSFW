using System;
using System.Net.Sockets;

namespace NSFW.Sockets
{
    /// <summary>
    /// Base class for client and server sockets
    /// </summary>
    public abstract class TcpSocket : IDisposable
    {
        /// <summary>
        /// Is the data available to receive
        /// </summary>
        public bool DataAvailable => Socket?.IsAvailable() ?? false;

        private protected Socket? Socket { get; set; } = new(SocketType.Stream, ProtocolType.Tcp);

        /// <summary>
        /// Basic socket constructor
        /// </summary>
        public TcpSocket() { }
        /// <summary>
        /// Basic socket constructor
        /// </summary>
        /// <param name="socket">Socket to create a managed TCP socket</param>
        public TcpSocket(Socket socket) =>
            Socket = socket;

        /// <summary>
        /// Send data
        /// </summary>
        /// <param name="data">Data to send</param>
        public abstract void Send(byte[]? data);
        /// <summary>
        /// Receive data
        /// </summary>
        /// <returns>Received data</returns>
        public abstract byte[]? Receive();

        /// <summary>
        /// Send data
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data to send</param>
        public void Send<T>(T data) =>
            Send(data?.Serialize());
        /// <summary>
        /// Receive data
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <returns>Received data</returns>
        public T? Receive<T>() =>
            Receive().Deserialize<T>() ?? default;

        /// <summary>
        /// Dispose Socket
        /// </summary>
        public void Dispose()
        {
            Socket?.Close();
            Socket?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
