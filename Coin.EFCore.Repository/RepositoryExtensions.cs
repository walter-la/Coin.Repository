using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EFCore.BulkExtensions;

namespace Coin.EFCore.Repository
{
    public static class RepositoryExtensions
    {
        public static int InsertSave<TEntity>(this DbContext db, TEntity entity) where TEntity : class
        {
            db.Set<TEntity>().Add(entity);
            return db.SaveChanges();
        }

        public static Task<int> InsertSaveAsync<TEntity>(this DbContext db, TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class
        {
            db.Set<TEntity>().Add(entity);
            return db.SaveChangesAsync(cancellationToken);
        }

        public static int UpdateSave<TEntity>(this DbContext db, TEntity entity) where TEntity : class
        {
            var entry = db.Entry(entity);
            if (entry.State != EntityState.Modified)
            {
                db.Set<TEntity>().Update(entity);
            }
            return db.SaveChanges();
        }

        public static Task<int> UpdateSaveAsync<TEntity>(this DbContext db, TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class
        {
            var entry = db.Entry(entity);
            if (entry.State != EntityState.Modified)
            {
                db.Set<TEntity>().Update(entity);
            }
            return db.SaveChangesAsync(cancellationToken);
        }

        public static int DeleteSave<TEntity>(this DbContext db, TEntity entity) where TEntity : class
        {
            db.Set<TEntity>().Remove(entity);
            return db.SaveChanges();
        }

        public static Task<int> DeleteSaveAsync<TEntity>(this DbContext db, TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class
        {
            db.Set<TEntity>().Remove(entity);
            return db.SaveChangesAsync(cancellationToken);
        }

        public static int DeleteSave<TEntity>(this DbContext db, IEnumerable<TEntity> entities) where TEntity : class
        {
            db.Set<TEntity>().RemoveRange(entities);
            return db.SaveChanges();
        }

        public static Task<int> DeleteSaveAsync<TEntity>(this DbContext db, IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class
        {
            db.Set<TEntity>().RemoveRange(entities);
            return db.SaveChangesAsync(cancellationToken);
        }

        public static void BulkInsertSave<TEntity>(this DbContext db, IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where TEntity : class
        {
            db.BulkInsert(entities, bulkConfig, progress);
        }

        public static Task BulkInsertSaveAsync<TEntity>(this DbContext db, IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where TEntity : class
        {
            return db.BulkInsertAsync(entities, bulkConfig, progress);
        }

        public static void BulkUpdateSave<TEntity>(this DbContext db, IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where TEntity : class
        {
            db.BulkUpdate(entities, bulkConfig, progress);
        }

        public static Task BulkUpdateSaveAsync<TEntity>(this DbContext db, IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where TEntity : class
        {
            return db.BulkInsertOrUpdateAsync(entities, bulkConfig, progress);
        }

        public static void BulkInsertOrUpdateSave<TEntity>(this DbContext db, IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where TEntity : class
        {
            db.BulkInsertOrUpdate(entities, bulkConfig, progress);
        }

        public static Task BulkInsertOrUpdateSaveAsync<TEntity>(this DbContext db, IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where TEntity : class
        {
            return db.BulkInsertOrUpdateAsync(entities, bulkConfig, progress);
        }
    }
}
