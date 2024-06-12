namespace Fluxera.StronglyTypedId.LiteDB.UnitTests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.LiteDB;
	using Fluxera.StronglyTypedId.LiteDB.UnitTests.Model;
	using global::LiteDB;
	using global::LiteDB.Async;
	using global::LiteDB.Queryable;
	using NUnit.Framework;

	[TestFixture]
	public class QueryTests
	{
		private LiteDatabaseAsync database;

		[SetUp]
		public void SetUp()
		{
			BsonMapper.Global.Entity<Person>().Id(x => x.Id);
			BsonMapper.Global.UseStronglyTypedId();

			this.database = new LiteDatabaseAsync($"{Guid.NewGuid():N}.db");
		}

		[TearDown]
		public void TearDown()
		{
			this.database?.Dispose();
		}

		[Test]
		public async Task ShouldFindByStronglyTypedID()
		{
			ILiteCollectionAsync<Person> collection = this.database.GetCollection<Person>();

			Person person = new Person
			{
				Id = PersonId.Create(Guid.NewGuid().ToString("N")),
				Name = "Tester"
			};

			await collection.InsertAsync(person);

			Person linqFilterResult = await collection
				.AsQueryable()
				.Where(x => x.Id == person.Id)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}
	}
}
