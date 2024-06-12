namespace Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests
{
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests.Model;
	using Microsoft.EntityFrameworkCore;
	using NUnit.Framework;

	[TestFixture]
	public class QueryTests
	{
#pragma warning disable NUnit1032
		private TestDbContext context;
#pragma warning restore NUnit1032

		[OneTimeSetUp]
		public void SetUp()
		{
			this.context = DbContextFactory.Generate();
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			this.context?.Dispose();
		}

		[Test]
		public async Task ShouldFindByStronglyTypedId()
		{
			Person linqFilterResult = await this.context
				.Set<Person>()
				.Where(x => x.Id == PersonId.Create("12345"))
				.FirstOrDefaultAsync();

			linqFilterResult.Should().NotBeNull();
		}

		[Ignore("Fix this later")]
		[Test]
		public async Task ShouldFindByValue()
		{
			Person linqFilterResult = await this.context
				.Set<Person>()
				.Where(x => x.Id == "12345")
				.FirstOrDefaultAsync();

			linqFilterResult.Should().NotBeNull();
		}
	}
}
