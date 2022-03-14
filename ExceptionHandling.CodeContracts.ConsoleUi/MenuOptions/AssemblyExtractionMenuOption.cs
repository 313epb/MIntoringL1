using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ExceptionHandling.CodeContracts.CodeAuthorInfo;

namespace ExceptionHandling.CodeContracts.ConsoleUi.MenuOptions
{
	/// <summary>
	/// Represents an menu option opening a <see cref="OpenFileDialog" /> and passing an <see cref="Assembly" /> to the
	/// <see cref="CodeAuthorInfoProvider" />.
	/// </summary>
	public class AssemblyExtractionMenuOption : MenuOption
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyExtractionMenuOption" />.
		/// </summary>
		public AssemblyExtractionMenuOption()
			: base(MenuResources.AssemblyExtractionMenuOptionName, new MenuCommand(ExecuteAction))
		{
		}

		/// <summary>
		/// Executes the <see cref="MenuOption.Command" />.
		/// </summary>
		protected override void DoExecute()
		{
			using (var openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = MenuResources.AssembliesFilter;
				openFileDialog.Multiselect = false;
				var dialogResult = openFileDialog.ShowDialog();

				if (dialogResult != DialogResult.OK)
				{
					return;
				}

				Command.Execute(openFileDialog.OpenFile());
			}

			//Handle assembly
		}

		private static void ExecuteAction(object o)
		{
			var fileStream = o as FileStream;

			if (fileStream == null)
			{
				throw new InvalidOperationException("Passed argument is not valid.");
			}

			var assemblyData = new byte[fileStream.Length];

			using (fileStream)
			{
				fileStream.Read(assemblyData, 0, assemblyData.Length);
			}

			var assembly = Assembly.Load(assemblyData);

			if (assembly == null)
			{
				throw new InvalidOperationException("Assembly cannot be null.");
			}

			var codeAuthorInfoProvider = new CodeAuthorInfoProvider(assembly);

			foreach (var attribute in codeAuthorInfoProvider.InfoElements)
			{
				Console.WriteLine(attribute);
			}
		}
	}
}