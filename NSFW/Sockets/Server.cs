using System.Net;
using System.Net.Sockets;

namespace NSFW.Sockets
{
    public class Server : TcpSocket
    {
        public List<Client> Clients { get; } = new();

        public bool IsStarted { get; private set; } = false;

        public override event Action OnConnect = () => { };

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
        /// <summary>
        /// Listen for connection
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Listen()
        {
            if (Socket == null || !IsStarted)
                throw new Exception("Can't listen: socket is null");

            Socket.Listen();

            Socket clientSocket = Socket.Accept() ?? throw new Exception("Client is null");
            var client = new Client(clientSocket);

            Clients.Add(client);
            OnConnect();
        }
        /// <summary>
        /// Listen all incoming connecions every 1 second
        /// </summary>
        public void ListenAsync()
        {
            Task.Run(() =>
            {
                while (IsStarted)
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
        public override void Send(byte[]? data)
        {
            if (data == null)
                return;
            for (int i = 0; i < Clients.Count; i++)
                DataExchange.Send(Clients[i].Socket, data);
        }
        /// <summary>
        /// Get data from all clients
        /// </summary>
        /// <returns></returns>
        public override byte[]? Receive()
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                var client = Clients[i];
                if (client == null)
                    continue;
                var data = DataExchange.Receive(client.Socket);
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
