// ReSharper disable InconsistentNaming

// ReSharper disable PossibleNullReferenceException

namespace Fluxera.StronglyTypedId.SystemTextJson.UnitTests
{
	using System.Text.Json;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class ConverterTests
	{
		private static readonly JsonSerializerOptions options;

		static ConverterTests()
		{
			options = new JsonSerializerOptions();
			options.UseStronglyTypedId();
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
			TestClass obj = JsonSerializer.Deserialize<TestClass>(JsonString, options);

			obj.PersonId.Should().Be(new PersonId("12345"));
		}

		[Test]
		public void ShouldSerialize()
		{
			string json = JsonSerializer.Serialize(TestInstance, options);

			json.Should().Be(JsonString);
		}
	}
}
