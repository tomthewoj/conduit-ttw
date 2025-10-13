using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Conduit.Modules.Users.Infrastructure.Persistence;
namespace Conduit.Tests;

public class DatabaseExistenceTests
{
	private const string RealDatabaseName = "Conduit";
	private const string ConnectionString =
		"Server=(localdb)\\MSSQLLocalDB;Database="+RealDatabaseName+";Trusted_Connection=True;";

	[Fact]
	public void DatabaseExists_ShouldSucceedOnlyIfDatabaseReallyExists()
	{
		using var dbContext = new UserDbContext(
			new DbContextOptionsBuilder<UserDbContext>()
				.UseSqlServer(ConnectionString)
				.Options);

		using var conn = dbContext.Database.GetDbConnection();
		conn.Open();

		using var cmd = conn.CreateCommand();
		cmd.CommandText = "SELECT COUNT(*) FROM sys.databases WHERE name = '" + RealDatabaseName + "'";
		var result = (int)cmd.ExecuteScalar();

		Assert.True(result > 0, "The database does not exist or the connection string is wrong.");
	}
}
