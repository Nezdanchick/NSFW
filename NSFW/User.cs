using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSFW
{
    /// <summary>
    /// Class for clear client and server management
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Username
        /// </summary>
        public static string Name { get => _name ?? Environment.UserName; set => _name = value; }
        private static string? _name;
        /// <summary>
        /// Is the client is connected
        /// </summary>
        public static bool Connected => Client.Connected;
    }
}
