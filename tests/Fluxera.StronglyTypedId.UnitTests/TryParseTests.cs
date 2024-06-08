#if NET7_0_OR_GREATER
namespace Fluxera.StronglyTypedId.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class TryParseTests
	{
		[Test]
		public void ShouldParseStringId()
		{
			bool success = StringId.TryParse("1234asdf", out StringId id);

			success.Should().BeTrue();
			id.Should().NotBeNull();
			id.Value.Should().Be("1234asdf");
		}

		[Test]
		[TestCase(null)]
		[TestCase("")]
		[TestCase("  ")]
		public void ShouldNotParseStringId(string value)
		{
			bool success = StringId.TryParse(value, out StringId id);

			success.Should().BeFalse();
			id.Should().BeNull();
		}

		[Test]
		public void ShouldParseNumericId()
		{
			bool success = IntegerId.TryParse("123", out IntegerId id);

			success.Should().BeTrue();
			id.Should().NotBeNull();
			id.Value.Should().Be(123);
		}

		[Test]
		[TestCase(null)]
		[TestCase("")]
		[TestCase("  ")]
		[TestCase("asdf")]
		[TestCase("1243-asdf")]
		public void ShouldNotParseNumericId(string value)
		{
			bool success = IntegerId.TryParse(value, out IntegerId id);

			success.Should().BeFalse();
			id.Should().BeNull();
		}

		[Test]
		public void ShouldParseGuidId()
		{
			bool success = GuidId.TryParse("0f445ea8c8884f47ab75bf7127a7f00d", out GuidId id);

			success.Should().BeTrue();
			id.Should().NotBeNull();
			id.Value.Should().Be(Guid.Parse("0f445ea8c8884f47ab75bf7127a7f00d"));
		}

		[Test]
		[TestCase(null)]
		[TestCase("")]
		[TestCase("  ")]
		[TestCase("0f445ea8f47f7127a7f00d")]
		public void ShouldNotParseGuidId(string value)
		{
			bool success = GuidId.TryParse(value, out GuidId id);

			success.Should().BeFalse();
			id.Should().BeNull();
		}
	}
}
#endif
