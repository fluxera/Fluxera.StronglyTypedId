namespace Fluxera.StronglyTypedId.UnitTests
{
	using FluentAssertions;
	using Fluxera.StronglyTypedId.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ConversionOperatorTests
	{
		[Test]
		public void ShouldConvertImplicitlyToValue()
		{
			StringId id = StringId.Create("12345");

			string result = id;

			result.Should().NotBeNull();
			result.Should().Be("12345");
		}

		[Test]
		public void ShouldConvertExplicitlyToId()
		{
			StringId result = (StringId)"12345";

			result.Should().NotBeNull();
			result.Value.Should().Be("12345");
		}
	}
}
