using NSFW;

var me = User.Current;
User other;

Console.WriteLine("Type 's' to start server, else client will be started");
me.IsClient = Console.ReadKey(true).KeyChar != 's';
Console.Clear();

if (me.IsClient)
{
    //Client

    Console.Write("Write address to connect: ");
    var endPoint = Console.ReadLine()?.Trim();
    other = me.Client.Connect(endPoint);
    Console.WriteLine("Connecting...");

    Info(other);

    Console.WriteLine("To close this program type \"exit\"");
    string line = "";
    while (line != "exit")
    {
        line = Console.ReadLine() ?? "";
        me.Send(line.Bytes());
    }
}
else
{
    // Server

    var address = me.Server.Start();
    Console.WriteLine(address);
    Console.WriteLine("Connecting...");
    other = me.Server.Listen();

    Info(other);

    string? line = "";
    while (line != "exit")
    {
        line = me.Receive()?.String();
        if (line != "")
            Console.WriteLine(line);
    }
}

static void Info(User user)
{
    Console.Clear();
    Console.WriteLine("Connected to:");
    Console.WriteLine(user);
    Console.WriteLine($"This chat with your friend {user.Name}!");
}
