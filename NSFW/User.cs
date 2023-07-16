using System.Net;

namespace NSFW
{
    public class User
    {
        public static List<User> All { get; } = new List<User>();
        public static User Current { get; } = new User(Environment.UserName, Address.GetGlobal());

        public string Name { get; private set; } = "Username";
        public IPAddress IPAddress { get; private set; } = Address.DefaultIP;

        public User() => All.Add(this);
        public User(string name, IPAddress address) : this()
        {
            Name = name;
            IPAddress = address;
        }

        public override string ToString() =>
            $"Name: {Name}\nIP: {IPAddress}";
    }
}