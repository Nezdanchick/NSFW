using NSFW;
using System.Net;

var me = User.Current;

Console.WriteLine(me);
Console.Write("Write IpAddress to connect: ");

string ip = Console.ReadLine() ?? "";
bool isCorrectIP = IPAddress.TryParse(ip, out IPAddress? address);

if (!isCorrectIP)
{
    Console.WriteLine("Invalid IP specified. Using localhost address...");
    address = Address.Default;
}
Console.WriteLine($"Connecting to {address}...");

// TODO: Connect to end point