using System.Net.Sockets;

namespace NSFW
{
    public static class DataExchange
    {
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
                    socket.Receive(buffer, SocketFlags.None);
                    result.Add(buffer[0]);
                }
            }
            catch (Exception) { }

            return result.ToArray();
        }
    }
}
