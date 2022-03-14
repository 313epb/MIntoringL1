using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdvancedCSharp.Types.Task1;
using AdvancedCSharp.Types.Task1.PerformanceMeasure;
using AdvancedCSharp.Types.Task1.Processing;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedCSharp.Types.Task1Tests
{
	[TestClass]
	public class SourceTests
	{
		#region CheckAndProceed

		/// <summary>
		/// Scenario Executes check and proceed of the <see cref="Source.CheckAndProceed" />
		/// Given Source data with valid items
		/// When Calls <see cref="Source.CheckAndProceed" />
		/// Then There must be logging events
		/// </summary>
		[TestMethod]
		public void CheckAndProceed_LogSource()
		{
			//Arrange
			var data = new List<InfoData>
			{
				new InfoData("Ivan", "Ivan"),
				new InfoData("Aleksey", "Alekseev")
			};
			var logExporter = new LogExporter<InfoData>(LogManager.GetLogger(typeof(Source)));
			var memoryAppender = new MemoryAppender();
			BasicConfigurator.Configure(memoryAppender);
			var source = new Source(logExporter);

			//Act
			source.CheckAndProceed(data);

			//Arrange
			Assert.IsTrue(memoryAppender.GetEvents().Any(loggingEvent => loggingEvent.Level == Level.Info));
		}

		/// <summary>
		/// Scenario Exectures <see cref="Source.CheckAndProceed" /> and measures perfomance with the help of
		/// <see cref="PerformanceMeasureExecutor" />
		/// Given Source data with valid items
		/// When Calls <see cref="Source.CheckAndProceed" />
		/// Then There must be results of performance measure and results of the <see cref="PerformanceMeasureInfo" /> must
		/// contains not default/null values
		/// </summary>
		[TestMethod]
		public void CheckAndProceed_PerformanceMeasure()
		{
			//Arrange
			var data = new List<InfoData>
			{
				new InfoData("Ivan", "Ivan"),
				new InfoData("Aleksey", "Alekseev")
			};
			var logExporter = new LogExporter<InfoData>(LogManager.GetLogger(typeof(Source)));
			var source = new Source(logExporter);
			var memoryAppender = new MemoryAppender();
			BasicConfigurator.Configure(memoryAppender);
			PerformanceMeasureExecutor performanceMeasureExecutor;

			//Act
			using (performanceMeasureExecutor = new PerformanceMeasureExecutor())
			{
				source.CheckAndProceed(data);
			}

			//Assert
			var performanceMeasureInfo = performanceMeasureExecutor.PerformanceMeasureInfo;
			Assert.IsTrue(performanceMeasureInfo.StartDataTime != default(DateTime) &&
						performanceMeasureInfo.CompleteDateTime != default(DateTime) &&
						performanceMeasureInfo.StartAllocatedMemory != default(long) &&
						performanceMeasureInfo.CompleteAllocatedMemory != default(long) &&
						performanceMeasureInfo.Elapsed != default(TimeSpan));
			Trace.WriteLine(performanceMeasureInfo.ToString());
		}

		#endregion

		#region Source

		/// <summary>
		/// Scenario Creates an instance of the <see cref="Source" /> with null values
		/// Given Null values for the constructor
		/// When Calls the constructor of the <see cref="Source" />
		/// Then Throws exception
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Source_NullValues()
		{
			//Arrange

			//Act
			var source = new Source(null);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Creates an instance of the <see cref="Source" /> with valid values
		/// Given Valid values
		/// When Calls the constructor of the <see cref="Source" />
		/// Then The instance must be created
		/// </summary>
		[TestMethod]
		public void Source_ValidValues()
		{
			//Arrange
			var logger = LogManager.GetLogger(typeof(Source));
			var logExporter = new LogExporter<InfoData>(logger);

			//Act
			var source = new Source(logExporter);

			//Assert
			Assert.IsNotNull(source);
		}

		#endregion
	}
}