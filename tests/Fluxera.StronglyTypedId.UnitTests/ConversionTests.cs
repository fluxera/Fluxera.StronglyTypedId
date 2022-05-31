namespace Fluxera.StronglyTypedId.UnitTests
{
	using FluentAssertions;
	using Fluxera.StronglyTypedId.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ConversionTests
	{
		[Test]
		public void ShouldConvertExplicitlyToId()
		{
			string value = "12345";
			StringId stringId = (StringId)value;

			stringId.Should().Be(new StringId("12345"));
		}
	}
}
