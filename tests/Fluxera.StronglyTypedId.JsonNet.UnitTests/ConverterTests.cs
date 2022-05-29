namespace Fluxera.StronglyTypedId.JsonNet.UnitTests
{
	using FluentAssertions;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class ConverterTests
	{
		[SetUp]
		public void SetUp()
		{
			JsonConvert.DefaultSettings = () =>
			{
				JsonSerializerSettings settings = new JsonSerializerSettings
				{
					Formatting = Formatting.None
				};
				settings.UseStronglyTypedId();
				return settings;
			};
		}

		public class TestClass
		{
			public PersonId PersonId { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			PersonId = new PersonId("12345")
		};

		private static readonly string JsonString = @"{""PersonId"":""12345""}";

		[Test]
		public void ShouldDeserialize()
		{
			TestClass obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			((object)obj.PersonId).Should().Be(new PersonId("12345"));
		}

		[Test]
		public void ShouldSerialize()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}
	}
}
