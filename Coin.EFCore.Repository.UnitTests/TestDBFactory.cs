using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coin.EFCore.Repository.UnitTests
{
	public static class TestDBFactory
	{
		public static ITestDB<TContext> Create<TContext>()
			 where TContext : DbContext
		{
			//return new SqliteTestDB<TContext>("DataSource=:memory:");

			var localDBConnStr = $"Server=(localdb)\\mssqllocaldb;Database=CoinTestDB_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true";
			return new SqlServerTestDB<TContext>(localDBConnStr);
		}
	}
}
