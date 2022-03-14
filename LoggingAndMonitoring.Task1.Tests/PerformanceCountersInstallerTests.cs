using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoggingAndMonitoring.Task1.Tests
{
	[TestClass]
	public class PerformanceCountersInstallerTests
	{
		/// <summary>
		/// Scenario Runs the console application LoggingAndMonitoring.Task1.PerformanceCountersInstaller
		/// Given The console appliacation LoggingAndMonitoring.Task1.PerformanceCountersInstaller
		/// When Runs the console application as a process
		/// Then There must be created a performance category with three performance counters
		/// </summary>
		[TestMethod]
		public void InstallPerformanceCounters()
		{
			//Arrange
			const string performanceCounterCategoryName = "LoggingAndMonitoring";
			var performanceCounterNames = new[] { "LogIn Successfull Count", "LogOff Successfull Count", "Album Watched Count" };
			var processInfo = new ProcessStartInfo("LoggingAndMonitoring.Task1.PerformanceCountersInstaller.exe")
			{
				UseShellExecute = true,
				Verb = "runas"
			};

			//Act
			var process = Process.Start(processInfo);
			process?.WaitForExit();

			//Assert
			var performanceCounterCategory =
				PerformanceCounterCategory.GetCategories().SingleOrDefault(pcc => pcc.CategoryName == performanceCounterCategoryName);

			Assert.IsNotNull(performanceCounterCategory);

			var performanceCounters = performanceCounterCategory.GetCounters();

			Assert.IsNotNull(performanceCounters);
			Assert.IsTrue(performanceCounters.Length == 3);
			Assert.IsTrue(performanceCounters.ToList().TrueForAll(pc => performanceCounterNames.Contains(pc.CounterName)));
		}
	}
}