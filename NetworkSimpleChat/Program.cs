using NSFW;

var me = User.Current;

Console.WriteLine("Type 's' to start server, else client will be started");
me.IsClient = Console.ReadKey().KeyChar != 's';
Console.Clear();

if (me.IsClient)
{
    //Client

    Console.Write("Write address to connect: ");
    var endPoint = Console.ReadLine()?.Trim();
    Console.WriteLine("Connecting...");
    var other = me.Client.Connect(endPoint);
    me.Send($"{me.Name} connected".Bytes());

    Info(other);

    Console.WriteLine("To close this program type \"exit\"");
    string line = "";
    while (line != "exit")
    {
        if (Console.KeyAvailable)
        {
            line = Console.ReadLine() ?? "";
            me.Send(line.Bytes());
        }

        var data = me.Receive();
        if (data == null)
            continue;
        line = data.String();
        Console.WriteLine(line);
    }
}
else
{
    // Server

    var address = me.Server.Start();
    SimpleChat.Clipboard.SetText(address.ToString());
    Console.WriteLine($"{address} (copied to clipboard)");

    me.Server.ListenAsync();
    Console.WriteLine("Messages of connected clients:");

    string? line = "";
    while (line != "exit")
    {
        var data = me.Receive();
        if (data == null)
            continue;
        line = data.String();
        me.Send(data);
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
