using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xunit;

namespace Coin.EFCore.Repository.UnitTests.Tests
{
	public class TransactionTests
	{
		[Fact]
		public void TransactionSucceedTest()
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

					using (var ts = new TransactionScope())
					{
						repo.Insert(sample);
						var inOfTransactionCount = repo.GetAll().Count();
						Assert.True(inOfTransactionCount == 1, "新增後，應該有資料");

						sample.SampleName = "DotNet";
						repo.Update(sample);
						ts.Complete();
					}
				}

				using (var db = testDB.CreateDbContext())
				{
					var repo = new RepositoryBase<SampleDbContext, Sample>(db);
					var dbItem = repo.GetAll().SingleOrDefault();
					Assert.True(dbItem?.SampleName == "DotNet", "交易完成後，應該有資料");
				}
			}
		}

		[Fact]
		public void TransactionFailedTest()
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

					using (var ts = new TransactionScope())
					{
						repo.Insert(sample);
						var inOfTransactionCount = repo.GetAll().Count();
						Assert.True(inOfTransactionCount == 1, "交易中應有新增後的一筆");
					}
				}

				using (var db = testDB.CreateDbContext())
				{
					var repo = new RepositoryBase<SampleDbContext, Sample>(db);

					var outOfTransactionCount = repo.GetAll().Count();
					Assert.True(outOfTransactionCount == 0, "交易沒commit，不應該有資料");
				}
			}
		}
	}
}
