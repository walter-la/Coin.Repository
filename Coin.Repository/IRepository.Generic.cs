using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coin.Repository
{
	public interface IRepository<TEntity> : IRepository
	{
		TEntity Get(Expression<Func<TEntity, bool>> predicate);
		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

		List<TEntity> GetAll();
		Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

		IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

		bool Exists(Expression<Func<TEntity, bool>> predicate);
		Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

		void Insert(TEntity entity);
		Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

		void Update(TEntity entity);
		Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

		void Delete(TEntity entity);
		Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

		void Delete(IEnumerable<TEntity> entities);
		Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

		void BulkInsert(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null);
		Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null);

		void BulkUpdate(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null);
		Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null);

		void BulkInsertOrUpdate(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null);
		Task BulkInsertOrUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null);
	}
}
