namespace Fluxera.StronglyTypedId.MongoDB.UnitTests
{
	using FluentAssertions;
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
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		public class TestClass
		{
			public PersonId PersonId { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			PersonId = new PersonId("12345")
		};

		private static readonly string JsonString = @"{ ""PersonId"" : ""12345"" }";

		[Test]
		public void ShouldDeserialize()
		{
			TestClass obj = BsonSerializer.Deserialize<TestClass>(JsonString);

			obj.PersonId.Should().Be(new PersonId("12345"));
		}

		[Test]
		public void ShouldSerialize()
		{
			string json = TestInstance.ToJson();

			json.Should().Be(JsonString);
		}
	}
}
