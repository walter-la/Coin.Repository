using Coin.Repository;
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

		public virtual IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
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

		public virtual void BulkInsert(IList<TEntity> entities)
			=> _db.BulkInsertSave(entities);

		public virtual Task BulkInsertAsync(IList<TEntity> entities)
			=> _db.BulkInsertSaveAsync(entities);

		public virtual void BulkUpdate(IList<TEntity> entities)
			=> _db.BulkUpdateSave(entities);

		public virtual Task BulkUpdateAsync(IList<TEntity> entities)
			=> _db.BulkInsertOrUpdateSaveAsync(entities);

		public virtual void BulkInsertOrUpdate(IList<TEntity> entities)
			=> _db.BulkInsertOrUpdateSave(entities);

		public virtual Task BulkInsertOrUpdateAsync(IList<TEntity> entities)
			=> _db.BulkInsertOrUpdateSaveAsync(entities);

	}
}