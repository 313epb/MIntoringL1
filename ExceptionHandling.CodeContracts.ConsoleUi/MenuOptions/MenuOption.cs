using System;
using System.Windows.Input;
using NLog;

namespace ExceptionHandling.CodeContracts.ConsoleUi.MenuOptions
{
	/// <summary>
	/// Represents a menu option.
	/// </summary>
	public class MenuOption
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Underlining string.
		/// </summary>
		protected static readonly string Underline = new string('*', 70);

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuOption" /> class.
		/// </summary>
		public MenuOption(string text, ICommand command)
		{
			Text = text;
			Command = command;
		}

		/// <summary>
		/// Text of the command.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Command to execute.
		/// </summary>
		public ICommand Command { get; set; }

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString() => Text;

		/// <summary>
		/// Executes the <see cref="Command" />.
		/// </summary>
		public void Execute()
		{
			WriteHeader();

			try
			{
				DoExecute();
			}
			catch (Exception exception)
			{
				Logger.Error(exception);
				ShowExceptionDetails(exception);
				throw;
			}
		}

		/// <summary>
		/// Executes the <see cref="Command" />.
		/// </summary>
		protected virtual void DoExecute()
		{
			Command.Execute(null);
		}

		/// <summary>
		/// Writes header title.
		/// </summary>
		protected virtual void WriteHeader()
		{
			Console.WriteLine(Underline);
			Console.WriteLine(Text);
			Console.WriteLine(Underline);
		}

		private static void ShowExceptionDetails(Exception exception)
		{
			Console.WriteLine(MenuResources.ExceptionTypeThrownMessageFormat, exception.GetType());
			Console.WriteLine(MenuResources.ExceptionMessageFormat, exception.Message);
			Console.WriteLine(MenuResources.ExceptionFormat, exception);
		}
	}
}