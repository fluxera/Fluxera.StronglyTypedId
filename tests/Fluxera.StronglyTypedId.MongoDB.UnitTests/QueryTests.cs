namespace Fluxera.StronglyTypedId.MongoDB.UnitTests
{
	using System;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.MongoDB.UnitTests.Model;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization.Conventions;
	using global::MongoDB.Driver;
	using global::MongoDB.Driver.Linq;
	using NUnit.Framework;

	[TestFixture]
	public class QueryTests
	{
		[Test]
		public async Task ShouldFindByStronglyTypedID()
		{
			ConventionPack pack = [];
			pack.UseStronglyTypedId();
			ConventionRegistry.Register("ConventionPack", pack, t => true);

			IMongoClient client = new MongoClient(GlobalFixture.ConnectionString);
			IMongoDatabase database = client.GetDatabase(GlobalFixture.Database);
			IMongoCollection<Person> collection = database.GetCollection<Person>("People");

			Person person = new Person
			{
				Id = PersonId.Create(ObjectId.GenerateNewId().ToString()),
				Name = "Tester"
			};

			await collection.InsertOneAsync(person);

			Person linqFilterResult = await collection
				.AsQueryable()
				.Where(x => x.Id == person.Id)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}
	}
}
