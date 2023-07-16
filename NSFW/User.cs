using System.Net;
using System.Net.Sockets;

namespace NSFW
{
    public class User : IDisposable
    {
        public static List<User> All { get; } = new List<User>();
        public static User Current { get; } = new User(Environment.UserName, Address.GetGlobal());

        public string Name { get; private set; } = "Username";
        public IPAddress IPAddress { get; private set; } = Address.DefaultIP;

        public User() => All.Add(this);
        public User(string name, IPAddress address) : this()
        {
            Name = name;
            IPAddress = address;
        }

        public void Connect(string? ip)
        {
            bool isCorrectIP = IPAddress.TryParse(ip, out IPAddress? address);

            if (!isCorrectIP || address == null)
            {
                Console.WriteLine("Invalid IP specified. Using local address...");
                address = Address.DefaultIP;
            }
            Console.WriteLine($"{Name} on {IPAddress} trying to connect to {address}...");

            using var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(address, Address.DefaultPort);
                Console.WriteLine($"Connected to {address}!");
                socket.Disconnect(true);
            }
            catch (SocketException e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        public override string ToString() =>
            $"Name: {Name}\nIP: {IPAddress}";

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}