using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSFW
{
	public partial class User
	{
        private static readonly User Current = new();

        private User()
		{
			// dispose on exit or cancel
			Console.CancelKeyPress +=
				(object? sender, ConsoleCancelEventArgs e) => Environment.Exit(0);
			AppDomain.CurrentDomain.ProcessExit +=
				(object? sender, EventArgs e) => Dispose();
		}
	}
}
