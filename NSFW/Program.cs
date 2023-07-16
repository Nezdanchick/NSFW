using NSFW;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using var me = User.Current;

Console.WriteLine(me);
Console.Write("Write IpAddress to connect: ");

var ip = Console.ReadLine();
me.Connect(ip);

Console.Write("Press any key to continue...");
Console.ReadKey();