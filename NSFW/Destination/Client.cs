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
            return new User("smb");
        }
    }
}