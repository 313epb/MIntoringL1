using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSUtil;

namespace LoggingAndMonitoring.Task1.Tests
{
	/// <summary>
	/// Tests for Part 3.1 with library LogParser.dll (installed by default to C:\Program Files (x86)\Log Parser 2.2)
	/// </summary>
	/// <remarks>
	/// COM library LogParser.dll was added to the LoggingAndMonitoring.Task1.Tests project by the Add Reference dialog on the
	/// COM tab.
	/// It is Interop.MSUtil.dll called as MSUtil in the References list of the project.
	/// As a log source the file 2016-10-24.log is being used.
	/// </remarks>
	[TestClass]
	public class LogParserTests
	{
		/// <summary>
		/// Scenario Executes Part 3.1 query to count log entries by entry levels
		/// Given LogParser query, format and query
		/// When Executes LogParser query
		/// Then There must be 3 records traced to the Test environment output
		/// </summary>
		[TestMethod]
		public void EntryCountByLevel()
		{
			//Arrange
			var logQuery = new LogQueryClass();
			var textLineFormat = new COMTextLineInputContextClass();
			const string query =
				@"SELECT Levels, COUNT(*) USING TRIM(SUBSTR(Text, 25, INDEX_OF(SUBSTR(Text, 25), ' '))) AS Levels FROM 2016-10-24.log GROUP BY Levels";

			//Act
			var logRecordSet = logQuery.Execute(query, textLineFormat);

			//Assert
			int i;

			for (i = 0; !logRecordSet.atEnd(); logRecordSet.moveNext(), i++)
			{
				Trace.WriteLine(logRecordSet.getRecord().toNativeString(" "));
			}

			Assert.IsTrue(i == 3);

			logRecordSet.close();
		}

		/// <summary>
		/// Scenario Executes Part 3.2 query to select only log error entries
		/// Given LogParser query, format and query
		/// When Executes LogParser query
		/// Then There must be 9 records traced to the Test environment outputs
		/// </summary>
		[TestMethod]
		public void ErrorList()
		{
			//Arrange
			var logQuery = new LogQueryClass();
			var textLineFormat = new COMTextLineInputContextClass();
			const string query = @"SELECT Text FROM 2016-10-24.log WHERE Text LIKE '%ERROR%'";
			//Act
			var logRecordSet = logQuery.Execute(query, textLineFormat);

			//Assert

			int i;

			for (i = 0; !logRecordSet.atEnd(); logRecordSet.moveNext(), i++)
			{
				Trace.WriteLine(logRecordSet.getRecord().toNativeString(" "));
			}

			Assert.IsTrue(i == 9);

			logRecordSet.close();
		}
	}
}