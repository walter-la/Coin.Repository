using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coin.Repository
{
    public interface IRepository
    {
		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

	}
}
