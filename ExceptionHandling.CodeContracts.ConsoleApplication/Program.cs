using System;
using ExceptionHandling.CodeContracts.ConsoleUi;
using ExceptionHandling.CodeContracts.ConsoleUi.MenuOptions;
using NLog;

namespace ExceptionHandling.CodeContracts.ConsoleApplication
{
	internal class Program
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		[STAThread]
		private static void Main()
		{
			try
			{
				var menuApplication = new MenuApplication(Resources.TaskName, new AssemblyExtractionMenuOption(),
					new AssemblyPathExtractionMenuOption());
				menuApplication.Run();
			}
			catch (Exception exception)
			{
				Logger.Error(exception);
				Environment.Exit(ExceptionCodesResolver.GetExceptionCode(exception.GetType()));
			}
		}
	}
}