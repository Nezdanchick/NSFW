﻿using System.Net.Sockets;

namespace NSFW
{
    public class NetDestination : IDisposable
    {
        public Socket? Socket { get; set; } = new(SocketType.Stream, ProtocolType.Tcp);

        public void Dispose()
        {
            Socket?.Close();
            Socket?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
