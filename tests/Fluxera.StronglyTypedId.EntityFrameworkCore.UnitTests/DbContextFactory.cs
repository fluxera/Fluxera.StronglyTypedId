﻿namespace Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests
{
	using System.Linq;
	using Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests.Model;

	public static class DbContextFactory
	{
		public static TestDbContext Generate()
		{
			PersonFactory.Initialize();

			TestDbContext context = new TestDbContext
			{
				SeedData = PersonFactory.Generate().ToArray(),
			};

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}
	}
}
