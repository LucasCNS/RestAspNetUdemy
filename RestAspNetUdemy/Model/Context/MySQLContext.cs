using Microsoft.EntityFrameworkCore;

namespace RestAspNetUdemy.Model.Context
{
	public class MySQLContext : DbContext
	{
		public MySQLContext()
		{

		}

		public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

		public DbSet<Person> People { get; set; }
	}
}
