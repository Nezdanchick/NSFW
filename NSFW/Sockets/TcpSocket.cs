using System.Net.Sockets;

namespace NSFW.Sockets
{
    public class TcpSocket : IDisposable
    {
        internal Socket? Socket { get; set; } = new(SocketType.Stream, ProtocolType.Tcp);

        public TcpSocket() { }
        public TcpSocket(Socket socket) =>
            Socket = socket;

        public void Send(byte[] data) =>
            DataExchange.Send(Socket, data);
        public byte[]? Receive() =>
            DataExchange.Receive(Socket);
        public void Dispose()
        {
            Socket?.Close();
            Socket?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
