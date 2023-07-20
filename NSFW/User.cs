using System.Net;
using System.Net.Sockets;
using NSFW.Destination;

namespace NSFW
{
    public class User : IDisposable
    {
        public static List<User> All { get; } = new List<User>();
        public static User Current { get; } = // Singletone
            new User(Environment.UserName, Address.LocalIP, Address.DefaultPort);

        public string Name { get; private set; } = "User";
        public IPAddress IPAddress { get; private set; } = Address.LocalIP;
        public int Port { get; private set; }
        public IPEndPoint IPEndPoint { get => new(IPAddress, Port); }

        public Socket? Socket => IsClient ? Client.Socket : Server.Socket;
        public bool IsClient;

        public Client Client
        {
            get
            {
                if (!IsClient)
                    IsClient = true;
                return _client;
            }
            private set => _client = value;
        }
        public Server Server
        {
            get
            {
                if (IsClient)
                    IsClient = false;
                return _server;
            }
            private set => _server = value;
        }

        private Client _client = new();
        private Server _server = new();

        #region Constructors
        internal User()
        {
            Console.CancelKeyPress +=
                (object? sender, ConsoleCancelEventArgs e) => Environment.Exit(0);
            // ProcessExit on CancelKeyPress event
            AppDomain.CurrentDomain.ProcessExit +=
                (object? sender, EventArgs e) => Dispose();
            // Dispose() on Exit(0) or Cancel
            All.Add(this);
        }
        internal User(string name, IPAddress address, int port) : this()
        {
            Name = name;
            IPAddress = address;
            Port = port;
        }
        internal User(string name, EndPoint endPoint)
            : this(name, Address.GetAddress(endPoint), Address.GetPort(endPoint)) { }
        #endregion
        #region Data Exchange
        public void Send(byte[] data) =>
            DataExchange.Send(Socket, data);
        public byte[]? Receive() =>
            DataExchange.Receive(Socket);
        #endregion

        public void Dispose()
        {
            if (Current == this) // current user
            {
                Console.WriteLine($"Goodbye, {Name}!");
                Thread.Sleep(1000);
            }
            _server?.Dispose();
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public override string ToString() =>
            $"Name: {Name}\nEnd: {IPEndPoint}";
    }
}