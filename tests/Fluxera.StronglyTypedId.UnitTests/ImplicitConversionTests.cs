namespace Fluxera.StronglyTypedId.UnitTests
{
	using FluentAssertions;
	using Fluxera.StronglyTypedId.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ImplicitConversionTests
	{
		[Test]
		public void ShouldConvertImplicitlyToId()
		{
			string value = "12345";
			StringId stringId = (StringId)value;

			stringId.Should().Be(new StringId("12345"));
		}

		[Test]
		public void ShouldConvertImplicitlyToValue()
		{
			StringId stringId = new StringId("12345");
			string value = stringId;

			value.Should().Be("12345");
		}
	}
}
