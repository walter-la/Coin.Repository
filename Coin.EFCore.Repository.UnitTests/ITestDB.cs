using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coin.EFCore.Repository.UnitTests
{
	public interface ITestDB<TContext> : IDisposable
		where TContext : DbContext
	{
		TContext CreateDbContext();
	}
}
