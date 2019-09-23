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
			=> _set.FirstOrDefault(predicate);

		public virtual Task<TEntity> GetAsync(
			Expression<Func<TEntity, bool>> predicate,
			CancellationToken cancellationToken = default(CancellationToken))
			=> _set.FirstOrDefaultAsync(predicate, cancellationToken);

		public virtual List<TEntity> GetAll() =>_set.ToList();

		public virtual Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
			=> _set.ToListAsync(cancellationToken);

		public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
			=> _set.Where(predicate);

		public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
			=> _set.Any(predicate);

		public virtual Task<bool> ExistsAsync(
			Expression<Func<TEntity, bool>> predicate,
			CancellationToken cancellationToken = default(CancellationToken))
			=> _set.AnyAsync(cancellationToken);

		public virtual void Insert(TEntity entity)
			=> _db.InsertSave(entity);

		public virtual Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
			=> _db.InsertSaveAsync(entity, cancellationToken);

		public virtual void Update(TEntity entity)
			=> _db.UpdateSave(entity);

		public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
			=> _db.UpdateSaveAsync(entity, cancellationToken);

		public virtual void Delete(TEntity entity)
			=> _db.DeleteSave(entity);

		public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
			=> _db.DeleteSaveAsync(entity, cancellationToken);

		public virtual void Delete(IEnumerable<TEntity> entities)
			=> _db.DeleteSave(entities);

		public virtual Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
			=> _db.DeleteSaveAsync(entities, cancellationToken);

		public virtual void BulkInsert(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
			=> _db.BulkInsertSave(entities, bulkConfig, progress);

		public virtual Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
			=> _db.BulkInsertSaveAsync(entities, bulkConfig, progress);

		public virtual void BulkUpdate(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
			=> _db.BulkUpdateSave(entities, bulkConfig, progress);

		public virtual Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
			=> _db.BulkInsertOrUpdateSaveAsync(entities, bulkConfig, progress);

		public virtual void BulkInsertOrUpdate(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
			=> _db.BulkInsertOrUpdateSave(entities, bulkConfig, progress);

		public virtual Task BulkInsertOrUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
			=> _db.BulkInsertOrUpdateSaveAsync(entities, bulkConfig, progress);

	}
}