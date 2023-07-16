using NSFW;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using var me = User.Current;

Console.WriteLine(me);
Console.WriteLine("Type 's' to start server, else client will be started");
bool isClient = Console.ReadKey().KeyChar != 's';
Console.Clear();

if (isClient)
    Client();
else
    Server();

void Client()
{
    Console.Write("Write IpAddress to connect: ");

    var ip = Console.ReadLine();
    me.Connect(ip);
}
void Server()
{
    me.Listen();
}

Console.Write("Press any key to continue...");
Console.ReadKey();