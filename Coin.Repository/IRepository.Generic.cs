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
		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

		List<TEntity> GetAll();
		Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

		IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);

		bool Exists(Expression<Func<TEntity, bool>> predicate);
		Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

		void Insert(TEntity entity);
		Task InsertAsync(TEntity entity);

		void Update(TEntity entity);
		Task UpdateAsync(TEntity entity);

		void Delete(TEntity entity);
		Task DeleteAsync(TEntity entity);

		void Delete(IEnumerable<TEntity> entities);
		Task DeleteAsync(IEnumerable<TEntity> entities);

		void BulkInsert(IList<TEntity> entities);
		Task BulkInsertAsync(IList<TEntity> entities);

		void BulkUpdate(IList<TEntity> entities);
		Task BulkUpdateAsync(IList<TEntity> entities);

		void BulkInsertOrUpdate(IList<TEntity> entities);
		Task BulkInsertOrUpdateAsync(IList<TEntity> entities);
	}
}
