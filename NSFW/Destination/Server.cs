using System.Net;
using System.Net.Sockets;

namespace NSFW.Destination
{
    public class Server : NetDestination
    {
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
            Thread.Sleep(500);

            if (client.RemoteEndPoint == null)
                throw new Exception("Client is null");

            Socket = client;

            return new User("User", client.RemoteEndPoint);
        }
    }
}
