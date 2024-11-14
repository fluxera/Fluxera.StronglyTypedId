namespace Fluxera.StronglyTypedId.MongoDB.UnitTests
{
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.MongoDB.UnitTests.Model;
	using global::MongoDB.Bson.Serialization.Conventions;
	using global::MongoDB.Driver;
	using global::MongoDB.Driver.Linq;
	using NUnit.Framework;

	[TestFixture]
	public class QueryTests
	{
		private IMongoCollection<Person> collection;

		[OneTimeSetUp]
		public async Task SetUp()
		{
			ConventionPack pack = [];
			pack.UseStronglyTypedId();
			ConventionRegistry.Register("ConventionPack", pack, t => true);

			IMongoClient client = new MongoClient(GlobalFixture.ConnectionString);
			IMongoDatabase database = client.GetDatabase(GlobalFixture.Database);
			this.collection = database.GetCollection<Person>("People");

			Person person = new Person
			{
				Id = PersonId.Create("6669b52802357c9886f6d24f"),
				Name = "Tester"
			};

			await collection.InsertOneAsync(person);
		}

		[Test]
		public async Task ShouldFindByStronglyTypedID()
		{
			Person linqFilterResult = await this.collection
				.AsQueryable()
				.Where(x => x.Id == PersonId.Create("6669b52802357c9886f6d24f"))
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Test]
		public async Task ShouldFindByValue()
		{
			Person linqFilterResult = await this.collection
				.AsQueryable()
				.Where(x => x.Id == "6669b52802357c9886f6d24f")
				.FirstOrDefaultAsync();

			linqFilterResult.Should().NotBeNull();
		}
	}
}
