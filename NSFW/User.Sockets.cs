using System;
using System.Threading;
using NSFW.Sockets;

namespace NSFW
{
    public partial class User
    {
        public static Client Client => Current._client;
        public static Server Server => Current._server;

        private readonly Client _client = new();
        private readonly Server _server = new();
    }
}