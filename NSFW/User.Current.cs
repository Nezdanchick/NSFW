using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSFW
{
	public partial class User : IDisposable
	{
		private static readonly User Current = new();

		/// <summary>
		/// Username
		/// </summary>
		public string Name { get => _name ?? _client.Name ?? "User"; set => _name = value; }
		private string? _name;

		private User()
		{
			Name = Environment.UserName;

			// dispose on exit or cancel
			Console.CancelKeyPress +=
				(object? sender, ConsoleCancelEventArgs e) => Environment.Exit(0);
			AppDomain.CurrentDomain.ProcessExit +=
				(object? sender, EventArgs e) => Dispose();
		}
		/// <summary>
		/// Dispose client and server
		/// </summary>
		public void Dispose()
		{
			_server?.Dispose();
			_client?.Dispose();
			GC.SuppressFinalize(this);
		}
		/// <summary>
		/// Convert to string
		/// </summary>
		/// <returns></returns>
		public override string ToString() =>
			$"User {Name}";
	}
}
