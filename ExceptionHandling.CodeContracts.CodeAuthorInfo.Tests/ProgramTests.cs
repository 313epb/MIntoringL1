using System;
using System.Diagnostics;
using System.IO;
using ExceptionHandling.CodeContracts.ConsoleApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExceptionHandling.CodeContracts.CodeAuthorInfo.Tests
{
	[TestClass]
	public class ProgramTests
	{
		private static readonly ConsoleKey QuitKey = ConsoleKey.Q;

		/// <summary>
		/// Scenario Runs the application ExceptionHandling.CodeContracts.ConsoleApplication that loads an assembly by specified
		/// path
		/// Given Menu option by specifying an assembly path
		/// When Gets code author info from specified assembly
		/// Then The console application must return 0 exit code
		/// </summary>
		[TestMethod]
		public void Main_SuccessfullAssemblyPathExtraction()
		{
			//Arrange
			const string menuOption = "1";
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExceptionHandling.CodeContracts.Data.dll");

			var process = new Process
			{
				StartInfo =
				{
					FileName = "ExceptionHandling.CodeContracts.ConsoleApplication.exe",
					UseShellExecute = false,
					RedirectStandardInput = true
				}
			};
			process.Start();

			//Act
			var standardInput = process.StandardInput;
			standardInput.AutoFlush = true;

			standardInput.WriteLine(menuOption);
			standardInput.WriteLine(path);
			standardInput.WriteLine(QuitKey);

			standardInput.Close();
			process.WaitForExit();

			//Assert
			Assert.IsTrue(process.ExitCode == 0);
			process.Close();
		}

		/// <summary>
		/// Scenario Runs the application ExceptionHandling.CodeContracts.ConsoleApplication that loads an not existing assembly
		/// Given Menu option by specifying an assembly path to not existing assembly
		/// When The application starts to load code author info from the specified path
		/// Then Returns <see cref="ExceptionCode.LoadCodeAuthorInfo" /> code
		/// </summary>
		[TestMethod]
		public void Main_ExceptionWhileLoadingCodeAuthorInfo()
		{
			//Arrange
			const string menuOption = "1";
			const string path = "NotExistingAssembly.dll";

			var process = new Process
			{
				StartInfo =
				{
					FileName = "ExceptionHandling.CodeContracts.ConsoleApplication.exe",
					UseShellExecute = false,
					RedirectStandardInput = true
				}
			};
			process.Start();

			//Act
			var standardInput = process.StandardInput;
			standardInput.AutoFlush = true;

			standardInput.WriteLine(menuOption);
			standardInput.WriteLine(path);
			standardInput.WriteLine(QuitKey);

			standardInput.Close();
			process.WaitForExit();

			//Assert
			Assert.IsTrue(process.ExitCode == (int) ExceptionCode.LoadCodeAuthorInfo);
			process.Close();
		}
	}
}