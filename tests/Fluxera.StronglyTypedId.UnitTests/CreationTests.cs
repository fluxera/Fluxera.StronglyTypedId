namespace Fluxera.StronglyTypedId.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class CreationTests
	{
		[Test]
		public void ShouldNotAllowNullValue_String()
		{
			Action action = () =>
			{
				StringId id = new StringId(null);
			};

			action.Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void ShouldNotAllowEmptyValue_String()
		{
			Action action = () =>
			{
				StringId id = new StringId(string.Empty);
			};

			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldNotAllowEmptyValue_Guid()
		{
			Action action = () =>
			{
				GuidId id = new GuidId(Guid.Empty);
			};

			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldNotAllowZeroValue_Numeric()
		{
			Action action = () =>
			{
				IntegerId id = new IntegerId(0);
			};

			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldNotAllowNegativeValue_Numeric()
		{
			Action action = () =>
			{
				IntegerId id = new IntegerId(-1);
			};

			action.Should().Throw<ArgumentException>();
		}
	}
}
