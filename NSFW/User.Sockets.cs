using System;
using System.Threading;
using NSFW.Sockets;

namespace NSFW
{
    public partial class User
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
    }
}