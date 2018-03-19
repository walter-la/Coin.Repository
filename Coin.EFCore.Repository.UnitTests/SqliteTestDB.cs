using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coin.EFCore.Repository.UnitTests
{
	public class SqliteTestDB<TContext> : ITestDB<TContext>
		where TContext : DbContext
	{
		private readonly SqliteConnection _connection;
		private readonly DbContextOptions<TContext> _options;
		public SqliteTestDB(string connectionString)
		{
			_connection = new SqliteConnection();
			_connection.Open();

			_options =
				new DbContextOptionsBuilder<TContext>()
				.UseSqlite(_connection)
				.Options;

			using (var db = CreateDbContext())
			{
				db.Database.EnsureCreated();
			}
		}

		public TContext CreateDbContext()
		{
			return (TContext)Activator.CreateInstance(typeof(TContext), _options);
		}

		public void Dispose()
		{
			_connection?.Dispose();
		}
	}


}
