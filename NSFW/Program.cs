using NSFW;

var me = User.Current;
User other;

Console.WriteLine("Type 's' to start server, else client will be started");
bool isClient = Console.ReadKey(true).KeyChar != 's';
Console.Clear();

if (isClient)
    Client();
else
    Server();

void Info(User user)
{
    Console.Clear();
    Console.WriteLine("Connected to:");
    Console.WriteLine(user);
    Console.WriteLine($"This chat with your friend {user.Name}!");
}
void Client()
{
    Console.Write("Write address to connect: ");

    var ip = Console.ReadLine();
    other = me.Connect(ip?.Trim());

    Info(other);

    Console.WriteLine("To close this program type \"exit\"");
    string line = "";
    while (line != "exit")
    {
        line = Console.ReadLine() ?? "";
        me.Send(line.Bytes());
    }
}
void Server()
{
    var endPoint = me.StartServer();

    Console.WriteLine($"Server started on {endPoint}\nWaiting...");

    other = me.Listen();

    Info(other);

    string? line = "";
    while (line != "exit")
    {
        line = me.Receive()?.String();
        if (line != null)
            Console.WriteLine(line);
    }
}
