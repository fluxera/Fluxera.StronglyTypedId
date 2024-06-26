﻿namespace Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests.Model;
	using Microsoft.EntityFrameworkCore;
	using NUnit.Framework;

	[TestFixture]
	public class ModelBuilderExtensionsTests
	{
		[Test]
		public async Task ShouldUseNameConverter()
		{
			await using TestDbContext context = DbContextFactory.Generate();

			List<Person> results = await context.People.Where(x => x.Id == (PersonId)"12345").ToListAsync();
			List<Person> people = context.Set<Person>().ToList();

			results.Should().BeEquivalentTo(context.SeedData);
			people.Should().BeEquivalentTo(context.SeedData);
		}
	}
}
