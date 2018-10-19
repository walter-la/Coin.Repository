using Coin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Coin.EFCore.Repository.UnitTests.Tests
{
	public class CRUDTests
	{
		[Fact]
		public void CRUDTest()
		{
			using (var testDB = TestDBFactory.Create<SampleDbContext>())
			{
				using (var db = testDB.CreateDbContext())
				{
					var repo = new RepositoryBase<SampleDbContext, Sample>(db);

					var sample = new Sample
					{
						SampleName = "Walter",
						UpdatedDate = DateTime.Now
					};
					repo.Insert(sample);
				}

				using (var db = testDB.CreateDbContext())
				{
					var repo = new RepositoryBase<SampleDbContext, Sample>(db);

					var samples = repo.GetAll();
					var sample = samples.FirstOrDefault();
					Assert.True(samples.Count == 1, "新增數量錯誤");
					Assert.True(sample.SampleName == "Walter", "沒有新增成功");

					sample.SampleName = "DoNet";
					repo.Update(sample);
				}

				using (var db = testDB.CreateDbContext())
				{
					var repo = new RepositoryBase<SampleDbContext, Sample>(db);

					var sample = repo.Get(x => x.SampleName == "DoNet");
					Assert.True(sample != null);

					repo.Delete(sample);

					sample = repo.Get(x => x.SampleId == sample.SampleId);
					Assert.True(sample == null);
				}
			}
		}

		[Fact]
		public async Task CRUDTestAsync()
		{
			using (var testDB = TestDBFactory.Create<SampleDbContext>())
			{
				using (var db = testDB.CreateDbContext())
				{
					var repo = new RepositoryBase<SampleDbContext, Sample>(db);

					var sample = new Sample
					{
						SampleName = "Walter",
						UpdatedDate = DateTime.Now
					};
					await repo.InsertAsync(sample);
				}

				using (var db = testDB.CreateDbContext())
				{
					var repo = new RepositoryBase<SampleDbContext, Sample>(db);

					var samples = await repo.GetAllAsync();
					var sample = samples.FirstOrDefault();
					Assert.True(samples.Count == 1, "新增數量錯誤");
					Assert.True(sample.SampleName == "Walter", "沒有新增成功");

					sample.SampleName = "DoNet";
					await repo.UpdateAsync(sample);
				}

				using (var db = testDB.CreateDbContext())
				{
					var repo = new RepositoryBase<SampleDbContext, Sample>(db);

					var sample = await repo.GetAsync(x => x.SampleName == "DoNet");
					Assert.True(sample != null);

					await repo.DeleteAsync(sample);

					sample = await repo.GetAsync(x => x.SampleId == sample.SampleId);
					Assert.True(sample == null);
				}
			}
		}
	}
}
