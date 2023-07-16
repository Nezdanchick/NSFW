using NSFW;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

var me = User.Current;

Console.WriteLine(me);
Console.Write("Write IpAddress to connect: ");

string ip = Console.ReadLine() ?? "";
bool isCorrectIP = IPAddress.TryParse(ip, out IPAddress? address);

if (!isCorrectIP || address == null)
{
    Console.WriteLine("Invalid IP specified. Using local address...");
    address = Address.DefaultIP;
}
Console.WriteLine($"Connecting to {address}...");

using var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
try
{
    await socket.ConnectAsync(address, Address.DefaultPort);
    Console.WriteLine($"Подключение к {address} установлено!");
    await socket.DisconnectAsync(true);
}
catch (SocketException e)
{
    Console.Error.WriteLine(e.Message);
}
Console.Write("Press any key to continue...");
Console.ReadKey();