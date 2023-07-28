﻿using System;
using System.Net.Sockets;

namespace NSFW.Sockets
{
    public abstract class TcpSocket : IDisposable
    {
        public abstract event Action OnConnect;

        internal Socket? Socket { get; private set; } = new(SocketType.Stream, ProtocolType.Tcp);

        public TcpSocket() { }
        public TcpSocket(Socket socket) =>
            Socket = socket;

        public virtual void Send(byte[]? data)
        {
            if (data != null)
                DataExchange.Send(Socket, data);
        }
        public virtual byte[]? Receive() =>
            DataExchange.Receive(Socket);
        public void Dispose()
        {
            Socket?.Close();
            Socket?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
