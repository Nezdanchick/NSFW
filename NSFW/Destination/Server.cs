using System.Net;
using System.Net.Sockets;

namespace NSFW.Destination
{
    public class Server : NetDestination
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

        public User Listen()
        {
            if (Socket == null)
                throw new Exception("Can't listen: socket is null");
            if (!IsStarted)
                Start(Address.DefaultPort);

            Socket.Listen();

            Socket client = Socket.Accept();

            if (client.RemoteEndPoint == null)
                throw new Exception("Client is null");

            Clients.Add(new Client(client));

            return new User("smb");
        }
        /// <summary>
        /// Send data to all clients
        /// </summary>
        /// <param name="data"></param>
        public new void Send(byte[] data)
        {
            foreach (Client client in Clients)
                DataExchange.Send(client.Socket, data);
        }
        /// <summary>
        /// Get data from all clients
        /// </summary>
        /// <returns></returns>
        public new byte[] Receive()
        {
            var bytes = new List<byte>();
            foreach (Client client in Clients)
            {
                var range = DataExchange.Receive(client.Socket);
                if (range != null)
                    bytes.AddRange(range);
            }
            return bytes.ToArray();
        }
    }
}
