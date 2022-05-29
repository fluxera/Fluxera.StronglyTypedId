namespace Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests
{
	using System;
	using System.Collections.Generic;
	using Bogus;

	public static class PersonFactory
	{
		public static IList<Person> Generate(int count)
		{
			return new Faker<Person>()
				.RuleFor(e => e.Id, (f, e) => f.Random.Guid().ToString())
				.RuleFor(e => e.PersonId, (f, e) => new PersonId("12345"))
				.Generate(count);
		}

		public static void Initialize()
		{
			Randomizer.Seed = new Random(62392);
		}
	}
}
