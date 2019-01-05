# Coin.Repository

## Introduction

Coin.Repository can help to easy do CURD actions.

## Code Samples

Basically, you can create about  your own RepositoryBase:

	public class CoinRepositoryGenericBase<TEntity> : RepositoryBase<CoinDbContext, TEntity> where TEntity : class
	{
		public CoinRepositoryGenericBase(CoinDbContext db) : base(db)
		{
		}
	}

Next register the RepositoryBase class in Startup:

```
services.AddTransient(typeof(IRepository<>), typeof(CoinRepositoryGenericBase<>));
```

Then you can inject the interface to use anywhere.

## Installation

Install-Package Coin.EFCore.Repository