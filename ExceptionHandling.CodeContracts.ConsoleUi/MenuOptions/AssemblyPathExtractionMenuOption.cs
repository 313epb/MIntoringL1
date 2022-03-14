using System;
using System.IO;
using System.Reflection;
using ExceptionHandling.CodeContracts.CodeAuthorInfo;

namespace ExceptionHandling.CodeContracts.ConsoleUi.MenuOptions
{
	/// <summary>
	/// Represents an menu option loading an <see cref="Assembly" /> by the specified path.
	/// </summary>
	public class AssemblyPathExtractionMenuOption : MenuOption
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyPathExtractionMenuOption" /> class.
		/// </summary>
		public AssemblyPathExtractionMenuOption()
			: base(MenuResources.AssemblyPathExtractionMenuOptionName, new MenuCommand(ExecuteAction))
		{
		}

		/// <summary>
		/// Executes the <see cref="MenuOption.Command" />.
		/// </summary>
		protected override void DoExecute()
		{
			Console.WriteLine(MenuResources.PressPathToAssemblyMessage);
			Command.Execute(Console.ReadLine());
		}

		private static void ExecuteAction(object o)
		{
			var assemblyName = o as string;

			if (string.IsNullOrWhiteSpace(assemblyName))
			{
				throw new InvalidOperationException("Passed argument is not valid.");
			}

			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName);

			var codeAuthorInfoProvider = new CodeAuthorInfoProvider(path);

			foreach (var attribute in codeAuthorInfoProvider.InfoElements)
			{
				Console.WriteLine(attribute);
			}
		}
	}
}