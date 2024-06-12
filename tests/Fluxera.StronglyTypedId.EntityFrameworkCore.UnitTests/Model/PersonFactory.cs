namespace Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests.Model
{
	using System;
	using System.Collections.Generic;
	using Bogus;

	public static class PersonFactory
	{
		public static IList<Person> Generate()
		{
			return new Faker<Person>()
				.RuleFor(e => e.Id, (_, _) => new PersonId("12345"))
				.RuleFor(e => e.Name, (f, _) => f.Name.FirstName())
				.Generate(1);
		}

		public static void Initialize()
		{
			Randomizer.Seed = new Random(37);
		}
	}
}
