﻿using NSFW.Destination;

namespace NSFW
{
    public class User : IDisposable
    {
        public static User Current { get; } = // Singletone
            new User(Environment.UserName);

        public string Name { get => Client.Name ?? _name; set => _name = value; }
        private string _name = "User";

        #region Client Server
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
        #endregion

        #region Constructors
        internal User()
        {
            Console.CancelKeyPress +=
                (object? sender, ConsoleCancelEventArgs e) => Environment.Exit(0);
            // ProcessExit on CancelKeyPress event
            AppDomain.CurrentDomain.ProcessExit +=
                (object? sender, EventArgs e) => Dispose();
            // Dispose() on ProcessExit or CancelKeyPress
        }
        internal User(string name) : this() =>
            Name = name;
        #endregion

        public void Send(byte[] data)
        {
            if (IsClient)
                Client.Send(data);
            else
                Server.Send(data);
        }

        public byte[]? Receive()
        {
            if (IsClient)
                return Client.Receive();
            else
                return Server.Receive();
        }

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
            $"User {Name}";
    }
}