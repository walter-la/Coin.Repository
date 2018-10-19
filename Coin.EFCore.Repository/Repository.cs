using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Coin.Repository;
using System.Threading.Tasks;
using System.Threading;

namespace Coin.EFCore.Repository
{
	public class RepositoryBase<TDbContext> : IRepository
		where TDbContext : DbContext
	{
		protected readonly TDbContext _db;
		public RepositoryBase(TDbContext db)
		{
			_db = db;
		}

		protected virtual IDbConnection GetConnection()
		{
			return _db.Database.GetDbConnection();
		}

		protected virtual int Execute(string sql, object param = null, IDbTransaction transaction = null)
		{
			var conn = GetConnection();
			return conn.Execute(sql, param, transaction);
		}

		public virtual int SaveChanges()
		{
			return _db.SaveChanges();
		}
		public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return _db.SaveChangesAsync(cancellationToken);
		}

	}
}
