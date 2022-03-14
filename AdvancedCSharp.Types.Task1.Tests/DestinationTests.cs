using System;
using System.Collections.Generic;
using AdvancedCSharp.Types.Task1;
using AdvancedCSharp.Types.Task1.Processing;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedCSharp.Types.Task1Tests
{
	[TestClass]
	public class DestinationTests
	{
		/// <summary>
		/// Scenario Creates an instance of the <see cref="Destination{T}" /> with null parameters
		/// Given Null parameters
		/// When Calls the constructor of the <see cref="Destination{T}" />
		/// Then Throws exception
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Destination_NullValues()
		{
			//Arrange

			//Act
			var destination = new Destination<InfoData>(null, null);

			//Arrange is handled by exception
		}

		/// <summary>
		/// Scenario Creates an instance of the <see cref="Destination{T}" /> with valid values
		/// Given Valid values for the constructor
		/// When Calls the constructor
		/// Then The instance must be created
		/// </summary>
		[TestMethod]
		public void Destination_ValidValues()
		{
			//Arrange
			var data = new List<InfoData>
			{
				new InfoData("Ivan", "Ivanov")
			};
			var logExporter = new LogExporter<InfoData>(LogManager.GetLogger(typeof(Destination<InfoData>)));

			//Act
			var destination = new Destination<InfoData>(data, logExporter);

			//Assert
			Assert.IsNotNull(destination);
		}
	}
}