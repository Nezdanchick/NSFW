using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NSFW.Sockets
{
    /// <summary>
    /// Wrapper over a socket that provides server capabilities
    /// </summary>
    public class Server : TcpSocket
    {
        /// <summary>
        /// Occurs when client connects
        /// </summary>
        public event Action OnClientConnected = () => { };
        /// <summary>
        /// EndPoint isn't null when server is started
        /// </summary>
        public IPEndPoint? EndPoint;
        /// <summary>
        /// All connected clients
        /// </summary>
        public List<Client> Clients { get; } = new();

        /// <summary>
        /// Shows if the server is started
        /// </summary>
        public bool IsStarted { get; private set; } = false;

        /// <summary>
        /// Shows if the server is listening connections
        /// </summary>
        public bool IsListening { get; private set; } = false;

        /// <summary>
        /// Start server
        /// </summary>
        /// <returns>IPEndPoint on which the server is started</returns>
        public void Start() =>
            Start(0);

        /// <summary>
        /// Start server on specified port
        /// </summary>
        /// <param name="port">Server listening port</param>
        /// <returns>IPEndPoint on which the server is started</returns>
        public void Start(int port)
        {
            if (IsStarted)
                return;

            IPEndPoint ipPoint = new(IPAddress.Any, port);
            Socket?.Bind(ipPoint);
            var address = Address.GetLocal();
            port = Address.GetPort(Socket?.LocalEndPoint);
            IsStarted = true;
            EndPoint = new IPEndPoint(address, port);
        }
        /// <summary>
        /// Listen for connection
        /// </summary>
        /// <exception cref="Exception">If server not started</exception>
        public void Listen()
        {
            if (Socket == null || !IsStarted)
                throw new Exception("Can't listen: start the server");

            Socket.Listen();

            try
            {
                Socket clientSocket = Socket.Accept();
                var client = new Client(clientSocket);

                Clients.Add(client);

                OnClientConnected();
            }
            catch { }
        }
        /// <summary>
        /// Listen all incoming connecions
        /// </summary>
        /// <param name="delay">delay in milliseconds</param>
        public void ListenAsync(int delay = 1000)
        {
            if (!IsListening)
                Task.Run(() =>
                {
                    IsListening = true;
                    while (IsStarted)
                    {
                        Listen();
                        Task.Delay(delay);
                    }
                    IsListening = false;
                });
        }
        /// <summary>
        /// Resends received data to all clients once
        /// </summary>
        public void Broadcast()
        {
            var data = Receive();
            if (data != null)
                Send(data);
        }
        /// <summary>
        /// Resends received data to all clients while connected
        /// </summary>
        public void BroadcastAsync(int delay = 20)
        {
            Task.Run(() =>
            {
                while (Socket != null)
                {
                    Broadcast();
                    Task.Delay(delay);
                }
            });
        }
        /// <summary>
        /// Send data to all clients
        /// </summary>
        /// <param name="data">Data to send</param>
        public override void Send(byte[]? data)
        {
            if (data == null)
                return;
            for (int i = 0; i < Clients.Count; i++)
                Clients[i].Send(data);
        }
        /// <summary>
        /// Receive data from all clients
        /// </summary>
        /// <returns>Received data from clients</returns>
        public override byte[]? Receive()
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                var client = Clients[i];
                if (client == null)
                    continue;
                var data = client.Receive();
                if (data != null)
                    return data;
            }
            return null;
        }
        /// <summary>
        /// Dispose server
        /// </summary>
        public new void Dispose()
        {
            IsStarted = false;
            base.Dispose();
        }
    }
}
