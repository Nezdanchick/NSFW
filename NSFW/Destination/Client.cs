using System.Net;

namespace NSFW.Destination
{
    public class Client : NetDestination
    {
        public User Connect(string? endPoint)
        {
            _ = IPEndPoint.TryParse(endPoint ?? "", out IPEndPoint? address);
            address ??= Address.DefaultEndPoint;
            try
            {
                Socket?.Connect(address);
                Thread.Sleep(500);
            }
            catch (Exception) { }
            return new User("User", address);
        }
    }
}