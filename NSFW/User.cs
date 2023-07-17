using System.Net;
using System.Net.Sockets;

namespace NSFW
{
    public class User : IDisposable
    {
        public static List<User> All { get; } = new List<User>();
        public static User Current { get; } = // Singletone
            new User(Environment.UserName, Address.LocalIP, Address.DefaultPort);

        public string Name { get; private set; } = "Username";
        public IPAddress IPAddress { get; private set; } = Address.LocalIP;
        public int Port { get; private set; }
        public IPEndPoint IPEndPoint { get => new(IPAddress, Port); }
        public bool IsDataAvailable { get => Socket.Available > 0; }

        public Socket Socket { get; private set; } = new(SocketType.Stream, ProtocolType.Tcp);

        private bool _isServerStarted = false;

        #region Constructors
        private User()
        {
            Console.CancelKeyPress +=
                (object? sender, ConsoleCancelEventArgs e) => Environment.Exit(0);
            // add Exit(0) to CancelKeyPress for ProcessExit event
            AppDomain.CurrentDomain.ProcessExit +=
                (object? sender, EventArgs e) => Dispose();
            // Dispose() on Exit(0) or Cancel
            All.Add(this);
        }
        private User(string name, IPAddress address, int port) : this()
        {
            Name = name;
            IPAddress = address;
            Port = port;
        }
        private User(string name, EndPoint endPoint)
            : this(name, Address.GetAddress(endPoint), Address.GetPort(endPoint)) { }
        #endregion
        #region Connection
        public User Connect(string? endPoint)
        {
            bool isCorrectIP = IPEndPoint.TryParse(endPoint ?? "", out IPEndPoint? address);
            if (!isCorrectIP || address == null)
                address = IPEndPoint;
            try
            {
                Socket.Connect(address);
                Thread.Sleep(500);
                Send(Name.Bytes());
            }
            catch (Exception) { }
            return new User(Receive()?.String() ?? "User", address);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IPEndPoint StartServer()
        {
            IPEndPoint ipPoint = new(IPAddress.Any, 0);
            Socket = new(SocketType.Stream, ProtocolType.Tcp); // if socket == client
            Socket.Bind(ipPoint);
            _isServerStarted = true;
            var address = Address.GetLocal();
            var port = Address.GetPort(Socket.LocalEndPoint);
            return new IPEndPoint(address, port);
        }
        public User Listen()
        {
            if (!_isServerStarted)
                throw new Exception("To listen incoming connections please start server first.");
            Socket.Listen();

            Socket client = Socket.Accept();
            Thread.Sleep(500);

            if (client.RemoteEndPoint == null)
                throw new Exception("Client is null"); 

            Socket = client;
            Send(Name.Bytes());

            return new User(Receive()?.String() ?? "User",
                Address.GetAddress(client.RemoteEndPoint),
                Address.GetPort(client.RemoteEndPoint)
                );
        }
        #endregion
        #region Data Exchange
        public void Send(byte[] data) =>
            Send(Socket, data);
        public byte[]? Receive() =>
            Receive(Socket);

        public static void Send(Socket socket, byte[] data)
        {
            try
            {
                socket.Send(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static byte[]? Receive(Socket socket)
        {
            if (socket.Available == 0)
                return null;

            var result = new List<byte>();
            var buffer = new byte[1];
            try
            {
                while (socket.Available > 0)
                {
                    socket.Receive(buffer);
                    result.Add(buffer[0]);
                }
            }
            catch (Exception) { }

            return result.ToArray();
        }
        #endregion

        public void Dispose()
        {
            if (Current == this) // current user
            {
                Console.WriteLine($"Goodbye, {Name}!");
                Thread.Sleep(1000);
            }

            Socket.Close();
            Socket.Dispose();
            GC.SuppressFinalize(this);
        }

        public override string ToString() =>
            $"Name: {Name}\nEnd: {IPEndPoint}";
    }
}