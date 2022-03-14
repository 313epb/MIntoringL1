using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace LoggingAndMonitoring.Task1.PerformanceCountersInstaller
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
			{
				Console.WriteLine("There was an error while creating performance counters.");

				var exception = eventArgs.ExceptionObject as Exception;

				if (exception != null)
				{
					Console.WriteLine("Exception details:");
					Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "Message: {0}", exception.Message));
				}

				Console.ReadLine();
			};

			Console.WriteLine("Installing performance counters for Logging and Monitoring module...");

			var category =
				PerformanceCounterCategory.GetCategories()
					.SingleOrDefault(pcc => pcc.CategoryName == "LoggingAndMonitoring");

			if (category != null)
			{
				PerformanceCounterCategory.Delete("LoggingAndMonitoring");
			}

			var collection = new CounterCreationDataCollection();

			var logInData = new CounterCreationData
			{
				CounterName = "LogIn Successfull Count",
				CounterHelp = "Log in successfull count.",
				CounterType = PerformanceCounterType.NumberOfItems32
			};

			var logOffData = new CounterCreationData
			{
				CounterName = "LogOff Successfull Count",
				CounterHelp = "Log off successfull count.",
				CounterType = PerformanceCounterType.NumberOfItems32
			};

			var albumWatchedData = new CounterCreationData
			{
				CounterName = "Album Watched Count",
				CounterHelp = "Album watched count.",
				CounterType = PerformanceCounterType.NumberOfItems32
			};

			collection.Add(logInData);
			collection.Add(logOffData);
			collection.Add(albumWatchedData);

			PerformanceCounterCategory.Create("LoggingAndMonitoring",
				"Performance counter category for the Logging and Monitoring module.", PerformanceCounterCategoryType.SingleInstance,
				collection);

			Console.WriteLine("Performance counters are successfully installed.");
		}
	}
}