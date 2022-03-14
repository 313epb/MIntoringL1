using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orm.EntityFramework.Northwind;
using Orm.EntityFramework.Northwind.Migrations;

namespace Orm.EntityFramework.DataAccess.Tests
{
	[TestClass]
	public class NorthwindDeploymentTests
	{
		private const string ConnectionString = "Orm.EntityFramework.Northwind";

		/// <summary>
		/// Scenario Initializes a Northwind database on the specified connection string to the latest version
		/// Given Connection string to the Northwind databases
		/// When Initializes the database with the default Entity Framework initialization tools
		/// Then The database must be successfully initialized and updated to the latest version
		/// </summary>
		[TestMethod]
		public void InitialToLatestVersion()
		{
			//Arrange
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<NorthwindDbContext, Configuration>(ConnectionString));

			//Act
			using (var dbContext = new NorthwindDbContextFactory().Create())
			{
				dbContext.Database.Initialize(false);
			}

			//Assert is handled by not throwing any exception
		}
	}
}