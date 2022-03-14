using System.Data.Entity.Migrations;

namespace Orm.EntityFramework.Northwind.Migrations
{
	public partial class Version11 : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.EmployeeCreditCards",
				c => new
				{
					CardNumber = c.String(false, 16),
					EmployeeId = c.Int(false),
					ExpirationDate = c.DateTime(false),
					CardHolderName = c.String()
				})
				.PrimaryKey(t => new {t.CardNumber, t.EmployeeId})
				.ForeignKey("dbo.Employees", t => t.EmployeeId)
				.Index(t => t.EmployeeId);
		}

		public override void Down()
		{
			DropForeignKey("dbo.EmployeeCreditCards", "EmployeeId", "dbo.Employees");
			DropIndex("dbo.EmployeeCreditCards", new[] {"EmployeeId"});
			DropTable("dbo.EmployeeCreditCards");
		}
	}
}