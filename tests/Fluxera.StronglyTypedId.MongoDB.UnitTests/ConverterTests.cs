namespace Fluxera.StronglyTypedId.MongoDB.UnitTests
{
	using FluentAssertions;
	using Fluxera.StronglyTypedId.MongoDB.UnitTests.Model;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using NUnit.Framework;

	[TestFixture]
	public class ConverterTests
	{
		static ConverterTests()
		{
			ConventionPack pack = new ConventionPack();
			pack.UseStronglyTypedId();
			ConventionRegistry.Register("ConventionPack", pack, _ => true);
		}

		public class TestClass
		{
			public PersonId PersonId { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			PersonId = new PersonId("6299e4fda14ed1025f7a413e")
		};

		private static readonly string JsonString = @"{ ""PersonId"" : ObjectId(""6299e4fda14ed1025f7a413e"") }";

		[Test]
		public void ShouldDeserialize()
		{
			TestClass obj = BsonSerializer.Deserialize<TestClass>(JsonString);

			obj.PersonId.Should().Be(new PersonId("6299e4fda14ed1025f7a413e"));
		}

		[Test]
		public void ShouldSerialize()
		{
			string json = TestInstance.ToJson();

			json.Should().Be(JsonString);
		}
	}
}
