using System;
using System.Net.Sockets;

namespace NSFW.Sockets
{
    public abstract class TcpSocket : IDisposable
    {
        public abstract event Action OnConnect;

        internal Socket? Socket { get; private set; } = new(SocketType.Stream, ProtocolType.Tcp);

        public TcpSocket() { }
        public TcpSocket(Socket socket) =>
            Socket = socket;

        public abstract void Send(byte[]? data);
        public abstract byte[]? Receive();

        public void Send<T>(T data) =>
            Send(data?.Serialize());
        public T? Receive<T>() =>
            Receive().Deserialize<T>() ?? default;

        public void Dispose()
        {
            Socket?.Close();
            Socket?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
