using NSFW;

var me = User.Current;

Console.WriteLine("Type 's' to start server, else client will be started");
me.IsClient = Console.ReadKey().KeyChar != 's';
Console.Clear();

me.Client.OnConnect += () =>
{
    Console.WriteLine("Connected to server!");
};
me.Server.OnConnect += () =>
{
    Console.WriteLine("Client connected!");
};

if (me.IsClient)
{
    //Client

    Console.Write("Write address to connect: ");
    var endPoint = Console.ReadLine()?.Trim();
    Console.WriteLine("Connecting...");
    me.Client.Connect(endPoint);
    me.Send($"{me.Name} connected".Serialize());

    Console.WriteLine("To close this program type \"exit\"");
    string line = "";
    while (line != "exit")
    {
        if (Console.KeyAvailable)
        {
            line = Console.ReadLine() ?? "";
            me.Send(line);
        }

        var data = me.Receive<string>();
        if (data == null)
            continue;
        line = data;
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
    while (me.Server.Clients.Count == 0);
    Console.WriteLine("Messages of connected clients:");

    string? line = "";
    while (line != "exit")
    {
        var data = me.Receive<string>();
        if (data == null)
            continue;
        line = data;
        me.Send(data);
        Console.WriteLine(line);
    }
}