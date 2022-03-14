using System.Data.Entity.Infrastructure;

namespace Orm.EntityFramework.Northwind
{
	/// <summary>
	/// Implementation of the <see cref="IDbContextFactory{TContext}" />.
	/// </summary>
	/// <remarks>
	/// Implemented for initialization a Northwind database only for deployment and migration parts.
	/// </remarks>
	public class NorthwindDbContextFactory : IDbContextFactory<NorthwindDbContext>
	{
		public NorthwindDbContext Create()
		{
			//Only for deployment and migration parts of the task will use a different database 
			//with the specified connection string
			return new NorthwindDbContext("Orm.EntityFramework.Northwind");
		}
	}
}