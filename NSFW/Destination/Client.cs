using System.Net;
using System.Net.Sockets;

namespace NSFW.Destination
{
    public class Client : NetDestination
    {
        public string? Name { get; private set; }

        public Client() : base() { }
        public Client(Socket socket) : base(socket) { }

        public User Connect(string? endPoint)
        {
            _ = IPEndPoint.TryParse(endPoint ?? "", out IPEndPoint? address);
            address ??= Address.DefaultEndPoint;
            try
            {
                Socket?.Connect(address);
            }
            catch (Exception) { }
            return new User("smb");
        }
    }
}