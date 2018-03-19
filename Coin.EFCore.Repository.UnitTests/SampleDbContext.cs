using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coin.EFCore.Repository.UnitTests
{
	public class SampleDbContext : DbContext
	{
		public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
		{
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public DbSet<Sample> Samples { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Sample>(e =>
			{
				e.Property(p => p.SampleId)
				 .HasValueGenerator<SequentialGuidValueGenerator>();
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
