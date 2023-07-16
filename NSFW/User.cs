using System.Net;
using System.Net.Sockets;

namespace NSFW
{
    public class User : IDisposable
    {
        public static List<User> All { get; } = new List<User>();
        public static User Current { get; } = new User(Environment.UserName, Address.DefaultIP);

        public string Name { get; private set; } = "Username";
        public IPAddress IPAddress { get; private set; } = Address.DefaultIP;

        public Socket Socket { get; private set; } = new(SocketType.Stream, ProtocolType.Tcp);

        public User() => All.Add(this);
        public User(string name, IPAddress address) : this()
        {
            Name = name;
            IPAddress = address;
        }

        public void Connect(string? endPoint)
        {
            bool isCorrectIP = IPEndPoint.TryParse(endPoint ?? "", out IPEndPoint? address);

            if (!isCorrectIP || address == null)
            {
                Console.WriteLine("Invalid IP specified. Using local address...");
                address = new IPEndPoint(IPAddress, Address.DefaultPort);
            }
            Console.WriteLine($"{Name} on {IPAddress} trying to connect to {address}...");

            try
            {
                Socket.Connect(address);
                Console.WriteLine($"Connected to {address}!");
            }
            catch (SocketException e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
        public void Listen()
        {
            IPEndPoint ipPoint = new(IPAddress.Any, 0);
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(ipPoint);
            Socket.Listen();
            Console.WriteLine($"Server started on {Address.GetLocal()}:{ipPoint.Port}\nWaiting...");

            using Socket client = Socket.Accept();
            Console.WriteLine($"Client address: {client.RemoteEndPoint}");
        }

        public override string ToString() =>
            $"Name: {Name}\nIP: {IPAddress}";

        public void Dispose()
        {
            if (Socket.Connected)
                Socket.Disconnect(true);
            Socket.Close();
            Socket.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}