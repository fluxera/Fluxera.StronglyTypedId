namespace Fluxera.StronglyTypedId.LiteDB.UnitTests
{
	using FluentAssertions;
	using global::LiteDB;
	using NUnit.Framework;

	[TestFixture]
	public class ConverterTests
	{
		static ConverterTests()
		{
			BsonMapper.Global.UseStronglyTypedId();
			BsonMapper.Global.Entity<TestClass>().Id(x => x.ID);
		}

		public class TestClass
		{
			public PersonId ID { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			ID = new PersonId("12345")
		};

		private static readonly string JsonString = @"{""_id"":""12345""}";

		[Test]
		public void ShouldDeserialize()
		{
			BsonDocument doc = (BsonDocument)JsonSerializer.Deserialize(JsonString);
			TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.ID.Should().Be(new PersonId("12345"));
		}

		[Test]
		public void ShouldSerialize()
		{
			BsonDocument doc = BsonMapper.Global.ToDocument(TestInstance);
			string json = JsonSerializer.Serialize(doc);

			json.Should().Be(JsonString);
		}
	}
}
