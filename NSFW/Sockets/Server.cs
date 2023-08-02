using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace NSFW.Sockets
{
    /// <summary>
    /// Wrapper over a socket that provides server capabilities
    /// </summary>
    public class Server : TcpSocket
    {
        /// <summary>
        /// All connected clients
        /// </summary>
        public List<Client> Clients { get; } = new();

        /// <summary>
        /// Shows if the server is started
        /// </summary>
        public bool IsStarted { get; private set; } = false;

        /// <summary>
        /// Start server
        /// </summary>
        /// <returns>IPEndPoint on which the server is started</returns>
        public IPEndPoint Start() =>
            Start(0);

        /// <summary>
        /// Start server on specified port
        /// </summary>
        /// <param name="port">Server listening port</param>
        /// <returns>IPEndPoint on which the server is started</returns>
        public IPEndPoint Start(int port)
        {
            IPEndPoint ipPoint = new(IPAddress.Any, port);
            Socket?.Bind(ipPoint);
            var address = Address.GetLocal();
            port = Address.GetPort(Socket?.LocalEndPoint);
            IsStarted = true;
            return new IPEndPoint(address, port);
        }
        /// <summary>
        /// Listen for connection
        /// </summary>
        /// <exception cref="Exception">If server not started</exception>
        public void Listen()
        {
            if (Socket == null || !IsStarted)
                throw new Exception("Can't listen: socket is null");

            Socket.Listen();

            Socket clientSocket = Socket.Accept() ?? throw new Exception("Client is null");
            var client = new Client(clientSocket);

            Clients.Add(client);
        }
        /// <summary>
        /// Listen all incoming connecions every 1 second
        /// </summary>
        public void ListenAsync()
        {
            Task.Run(() =>
            {
                while (IsStarted)
                {
                    Listen();
                    Task.Delay(1000);
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
