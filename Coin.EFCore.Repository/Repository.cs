using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Coin.Repository;
using System.Threading.Tasks;

namespace Coin.EFCore.Repository
{
	public class Repository : IRepository
	{
		protected readonly DbContext _db;
		public Repository(DbContext db)
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

		protected virtual int SaveChanges()
		{
			return _db.SaveChanges();
		}
		protected virtual Task<int> SaveChangesAsync()
		{
			return _db.SaveChangesAsync();
		}

	}
}
