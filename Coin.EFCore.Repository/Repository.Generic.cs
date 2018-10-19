using Coin.Repository;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coin.EFCore.Repository
{
	public class RepositoryBase<TDbContext, TEntity> : RepositoryBase<TDbContext>, IRepository, IRepository<TEntity>
		where TDbContext : DbContext
		where TEntity : class
	{
		protected readonly DbSet<TEntity> _set;

		public RepositoryBase(TDbContext db) : base(db)
		{
			_set = db.Set<TEntity>();
		}

		public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
		{
			return _set.FirstOrDefault(predicate);
		}
		public virtual Task<TEntity> GetAsync(
			Expression<Func<TEntity, bool>> predicate, 
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return _set.FirstOrDefaultAsync(predicate, cancellationToken);
		}

		public virtual List<TEntity> GetAll()
		{
			return _set.ToList();
		}
		public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return _set.ToListAsync(cancellationToken);
		}

		public virtual IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
		{
			return _set.Where(predicate);
		}


		public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
		{
			return _set.Any(predicate);
		}
		public Task<bool> ExistsAsync(
			Expression<Func<TEntity, bool>> predicate, 
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return _set.AnyAsync(cancellationToken);
		}


		public virtual void Insert(TEntity entity)
		{
			_set.Add(entity);
			SaveChanges();
		}
		public Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
		{
			_set.Add(entity);
			return SaveChangesAsync();
		}

		public virtual void Update(TEntity entity)
		{
			var entry = _db.Entry(entity);
			if (entry.State != EntityState.Modified)
			{
				_set.Update(entity);
			}
			SaveChanges();
		}
		public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
		{
			var entry = _db.Entry(entity);
			if (entry.State != EntityState.Modified)
			{
				_set.Update(entity);
			}
			return SaveChangesAsync();
		}

		public virtual void Delete(TEntity entity)
		{
			_set.Remove(entity);
			SaveChanges();
		}
		public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
		{
			_set.Remove(entity);
			return SaveChangesAsync();
		}

		public virtual void Delete(IEnumerable<TEntity> entities)
		{
			_set.RemoveRange(entities);
			SaveChanges();
		}
		public Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
		{
			_set.RemoveRange(entities);
			return SaveChangesAsync();
		}

		public virtual void BulkInsert(IList<TEntity> entities)
		{
			_db.BulkInsert(entities);
		}
		public Task BulkInsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
		{
			return _db.BulkInsertAsync(entities);
		}

		public virtual void BulkUpdate(IList<TEntity> entities)
		{
			_db.BulkUpdate(entities);
		}
		public Task BulkUpdateAsync(IList<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
		{
			return _db.BulkInsertOrUpdateAsync(entities);
		}

		public virtual void BulkInsertOrUpdate(IList<TEntity> entities)
		{
			_db.BulkInsertOrUpdate(entities);
		}
		public Task BulkInsertOrUpdateAsync(IList<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
		{
			return _db.BulkInsertOrUpdateAsync(entities);
		}

	}
}