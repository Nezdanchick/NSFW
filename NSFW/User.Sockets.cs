using System;
using System.Threading;
using NSFW.Sockets;

namespace NSFW
{
    public partial class User : IDisposable
    {
        /// <summary>
        /// Client representation
        /// </summary>
        public static Client Client => Current._client;
        /// <summary>
        /// Server representation
        /// </summary>
        public static Server Server => Current._server;

        private readonly Client _client = new();
        private readonly Server _server = new();

        /// <summary>
        /// Dispose client and server
        /// </summary>
        public void Dispose()
        {
            _server?.Dispose();
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}