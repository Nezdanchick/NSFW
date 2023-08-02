using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NSFW
{
    /// <summary>
    /// Class for transmitting data over sockets
    /// </summary>
    public static class DataExchange
    {
        /// <summary>
        /// Value, represents availability of data on the socket
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static bool IsAvailable(this Socket socket) =>
            socket.Available > 0;
        /// <summary>
        /// Send data using socket
        /// </summary>
        /// <param name="socket">Connected socket</param>
        /// <param name="data">Data to send</param>
        /// <exception cref="Exception">If socket is null</exception>
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
        /// <summary>
        /// Receive data from socket
        /// </summary>
        /// <param name="socket">Connected socket</param>
        /// <returns>Received data</returns>
        /// <exception cref="Exception"></exception>
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
