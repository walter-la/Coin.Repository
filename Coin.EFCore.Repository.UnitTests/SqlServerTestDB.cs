using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coin.EFCore.Repository.UnitTests
{
	public class SqlServerTestDB<TContext> : ITestDB<TContext>
		 where TContext : DbContext
	{
		private readonly DbContextOptions<TContext> _options;
		public SqlServerTestDB(string connectionString)
		{
			_options =
				new DbContextOptionsBuilder<TContext>()
				.UseSqlServer(connectionString)
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
			using (var db = CreateDbContext())
			{
				db.Database.EnsureDeleted();
			}
		}
	}
}
