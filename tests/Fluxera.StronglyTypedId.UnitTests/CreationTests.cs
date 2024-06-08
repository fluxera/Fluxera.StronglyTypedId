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

#if NET7_0_OR_GREATER
		[Test]
		public void ShouldCreateStringId()
		{
			StringId id = StringId.Create("1234");

			id.Should().NotBeNull();
			id.Value.Should().Be("1234");
		}

		[Test]
		public void ShouldCreateIntegerId()
		{
			IntegerId id = IntegerId.Create(1234);

			id.Should().NotBeNull();
			id.Value.Should().Be(1234);
		}

		[Test]
		public void ShouldCreateGuidId()
		{
			Guid value = Guid.NewGuid();
			GuidId id = GuidId.Create(value);

			id.Should().NotBeNull();
			id.Value.Should().Be(value);
		}
#endif
	}
}
