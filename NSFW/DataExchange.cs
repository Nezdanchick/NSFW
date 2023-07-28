using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NSFW
{
    public static class DataExchange
    {
        /// <summary>
        /// Value, represents availability of data on the socket
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static bool IsAvailable(this Socket socket) =>
            socket.Available > 0;

        public static void Send(Socket? socket, byte[] data)
        {
            if (socket == null)
                throw new Exception("Can't send: socket is null");
            try
            {
                socket.Send(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static byte[]? Receive(Socket? socket)
        {
            if (socket == null)
                throw new Exception("Can't receive: socket is null");
            var result = new List<byte>();
            var buffer = new byte[1];
            try
            {
                while (socket.IsAvailable())
                {
                    int received = socket.Receive(buffer);
                    if (received != 0)
                        result.Add(buffer[0]);
                }
            }
            catch (Exception) { }

            if (result.Count == 0)
                return null;
            return result.ToArray();
        }
    }
}
