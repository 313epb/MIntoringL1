using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using ExceptionHandling.CodeContracts.ConsoleUi.MenuOptions;

namespace ExceptionHandling.CodeContracts.ConsoleUi
{
	/// <summary>
	/// Console menu driven application.
	/// </summary>
	public class MenuApplication
	{
		/// <summary>
		/// Underlining string.
		/// </summary>
		protected static readonly string Underline = new string('*', 70);

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuApplication" /> class.
		/// </summary>
		/// <param name="header">Header title.</param>
		/// <param name="menuOptions">Menu option collection.</param>
		public MenuApplication(string header, params MenuOption[] menuOptions)
		{
			Header = header;
			MenuOptions = new List<MenuOption>(menuOptions);
		}

		/// <summary>
		/// Key to close the application.
		/// </summary>
		protected ConsoleKey QuitKey { get; set; } = ConsoleKey.Q;

		/// <summary>
		/// Enumerates menu option collection.
		/// </summary>
		public IList<MenuOption> MenuOptions { get; }

		/// <summary>
		/// Determines whether the applicatoin should quit or not.
		/// </summary>
		protected bool Quit { get; set; }

		/// <summary>
		/// Header title of the application.
		/// </summary>
		public string Header { get; }

		/// <summary>
		/// Runs the application.
		/// </summary>
		public void Run()
		{
			while (!Quit)
			{
				Display();
				SelectMenuOption();
				Continue();
			}
		}

		/// <summary>
		/// Continues execution of the <see cref="MenuApplication" /> after completion of an <see cref="ICommand" />.
		/// </summary>
		protected void Continue()
		{
			if (Quit)
			{
				return;
			}

			Console.WriteLine();
			Console.WriteLine(MenuResources.MenuApplicationContinueMessageFormat, QuitKey);

			string key = Console.ReadLine();

			if (key == QuitKey.ToString())
			{
				Quit = true;
			}
		}

		#region SelectMenuOption

		/// <summary>
		/// Selects a menu option from the <see cref="MenuOptions" />.
		/// </summary>
		protected void SelectMenuOption()
		{
			int selectedMenuOption = ReadSelectedMenuOption();

			if (selectedMenuOption == -1)
			{
				return;
			}

			MenuOptions[selectedMenuOption].Execute();
		}

		private int ReadSelectedMenuOption()
		{
			int selectedOptionIndex = -1;

			while (!Quit && !TrySelectMenuOption(selectedOptionIndex))
			{
				string key = Console.ReadLine();

				if (key == QuitKey.ToString())
				{
					Quit = true;
					return -1;
				}

				if (!int.TryParse(key, out selectedOptionIndex))
				{
					return selectedOptionIndex;
				}
			}

			return selectedOptionIndex;
		}

		private bool TrySelectMenuOption(int selectedMenuOptionIndex)
		{
			return selectedMenuOptionIndex >= 0 && selectedMenuOptionIndex < MenuOptions.Count;
		}

		#endregion

		#region Display

		/// <summary>
		/// Template method for displaying
		/// </summary>
		public void Display()
		{
			WriteHeader();
			WriteMenuOptions();
			WriteFooter();
		}

		/// <summary>
		/// Writes header title.
		/// </summary>
		protected virtual void WriteHeader()
		{
			Console.WriteLine(Underline);
			Console.WriteLine(Header);
			Console.WriteLine(Underline);
		}

		/// <summary>
		/// Writes menu options.
		/// </summary>
		protected void WriteMenuOptions()
		{
			for (var i = 0; i < MenuOptions.Count; i++)
			{
				WriteMenuOption(MenuOptions[i], i);
			}
		}

		/// <summary>
		/// Writes a menu option in the list of menu options.
		/// </summary>
		/// <param name="menuOption">Menu option to write.</param>
		/// <param name="index">Index of the <paramref name="menuOption" /> in the <see cref="MenuOptions" />.</param>
		protected virtual void WriteMenuOption(MenuOption menuOption, int index)
		{
			Console.WriteLine(string.Format(CultureInfo.CurrentCulture, MenuResources.MenuOptionFormat, index,
				MenuOptions[index]));
		}

		/// <summary>
		/// Writes footer title.
		/// </summary>
		protected virtual void WriteFooter()
		{
			Console.WriteLine();
			Console.WriteLine(MenuResources.MenuApplicationFooterFormat, QuitKey);
		}

		#endregion
	}
}