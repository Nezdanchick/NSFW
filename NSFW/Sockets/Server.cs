using System.Net;
using System.Net.Sockets;

namespace NSFW.Sockets
{
    public class Server : TcpSocket
    {
        public List<Client> Clients { get; } = new();

        public bool IsStarted { get; private set; } = false;

        public IPEndPoint Start() =>
            Start(0);
        public IPEndPoint Start(int port)
        {
            IPEndPoint ipPoint = new(IPAddress.Any, port);
            Socket?.Bind(ipPoint);
            var address = Address.GetLocal();
            port = Address.GetPort(Socket?.LocalEndPoint);
            IsStarted = true;
            return new IPEndPoint(address, port);
        }

        public void Listen()
        {
            if (Socket == null || !IsStarted)
                throw new Exception("Can't listen: socket is null");

            Socket.Listen();

            Socket client = Socket.Accept();

            if (client.RemoteEndPoint == null)
                throw new Exception("Client is null");

            Clients.Add(new Client(client));
        }
        public void ListenAsync()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Listen();
                    Task.Delay(1000);
                }
            });
        }
        /// <summary>
        /// Send data to all clients
        /// </summary>
        /// <param name="data"></param>
        public new void Send(byte[] data)
        {
            for (int i = 0; i < Clients.Count; i++)
                DataExchange.Send(Clients[i].Socket, data);
        }
        /// <summary>
        /// Get data from all clients
        /// </summary>
        /// <returns></returns>
        public new byte[]? Receive()
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                var data = DataExchange.Receive(Clients[i].Socket);
                if (data != null)
                    return data;
            }
            return null;
        }
        public new void Dispose()
        {
            IsStarted = false;
            base.Dispose();
        }
    }
}
