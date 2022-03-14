using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedCSharp.Types.Task1;
using AdvancedCSharp.Types.Task1.Processing;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedCSharp.Types.Task1Tests.Processing
{
	[TestClass]
	public class LogExporterTests
	{
		#region Process

		/// <summary>
		/// Scnerario Calls <see cref="LogExporter{T}.Process" /> method of the <see cref="LogExporter{T}" /> to log source data
		/// Given Valid source data with two items
		/// When Calls <see cref="LogExporter{T}.Process" />
		/// Then There must be logging event
		/// </summary>
		[TestMethod]
		public void Process_LogSource()
		{
			//Arrange
			var data = new List<InfoData>
			{
				new InfoData("Ivan", "Ivanov"),
				new InfoData("Aleksey", "Alekseev")
			};
			var logExporter = new LogExporter<InfoData>(LogManager.GetLogger(typeof(Source)));
			var memoryAppender = new MemoryAppender();
			BasicConfigurator.Configure(memoryAppender);

			//Act
			logExporter.Process(data);

			//Assert
			Assert.IsTrue(memoryAppender.GetEvents().Any(loggingEvent => loggingEvent.Level == Level.Info));
		}

		#endregion

		#region LogExporter

		/// <summary>
		/// Scenario Creates an instance of the <see cref="LogExporter{T}" /> with null <see cref="ILog" /> parameter
		/// Given Null value of <see cref="ILog" /> parameter for the constructor of <see cref="LogExporter{T}" />
		/// When Calls the constructor of <see cref="LogExporter{T}" />
		/// Then Throws exception
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void LogExporter_NullLogger()
		{
			//Arrange

			//Act
			var logExporter = new LogExporter<InfoData>(null);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Creates an instance of the <see cref="LogExporter{T}" /> with valid values
		/// Given Valid values for the constructor of the <see cref="LogExporter{T}" />
		/// When Calls the constructor of <see cref="LogExporter{T}" />
		/// Then The instances must be created
		/// </summary>
		[TestMethod]
		public void LogExport_ValidValues()
		{
			//Arrange
			var logger = LogManager.GetLogger(typeof(LogExporter<InfoData>));

			//Act
			var logExporter = new LogExporter<InfoData>(logger);

			//Assert
			Assert.IsNotNull(logExporter);
		}

		#endregion
	}
}