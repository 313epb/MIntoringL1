using System.Data.Entity.Migrations;

namespace Orm.EntityFramework.Northwind.Migrations
{
	public partial class Version13 : DbMigration
	{
		public override void Up()
		{
			RenameTable("dbo.Region", "Regions");
			AddColumn("dbo.Customers", "EstablishmentDate", c => c.DateTime());
		}

		public override void Down()
		{
			DropColumn("dbo.Customers", "EstablishmentDate");
			RenameTable("dbo.Regions", "Region");
		}
	}
}