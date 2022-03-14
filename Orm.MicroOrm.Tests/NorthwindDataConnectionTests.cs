using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orm.MicroOrm.Tests
{
	[TestClass]
	public class NorthwindDataConnectionTests
	{
		/// <summary>
		/// Scenario Creates an instance of the <see cref="NorthwindDataConnection" /> to work with the Northwind database
		/// Given Specified in the App.config connection string
		/// When Calls the constructor of the <see cref="NorthwindDataConnection" />
		/// Then Created instance of the <see cref="NorthwindDataConnection" /> is not null
		/// </summary>
		[TestMethod]
		public void NorthwindDataConnection_OpenConnection()
		{
			//Arrange
			string connectionString = ConfigurationManager.ConnectionStrings["Orm.Northwind"].Name;

			//Act
			using (new NorthwindDataConnection(connectionString))
			{
			}

			//Assert is handled by not throwing any exception
		}
	}
}