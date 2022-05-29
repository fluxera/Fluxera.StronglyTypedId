namespace Fluxera.StronglyTypedId.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class EqualsOperatorsTests
	{
		private static IEnumerable<object[]> OperatorTestData = new List<object[]>
		{
			new object[]
			{
				null!,
				null!,
				true
			},
			new object[]
			{
				new CustomerId("12345"),
				new CustomerId("12345"),
				true
			},
			new object[]
			{
				new CustomerId("12345"),
				new CustomerId("12346"),
				false
			},
			new object[]
			{
				new CustomerId("12345"),
				null!,
				false
			}
		};

		private static IEnumerable<object[]> OperatorPrimitiveTestData = new List<object[]>
		{
			new object[]
			{
				new CustomerId("12345"),
				new CustomerId("12345"),
				true
			}
		};

		[Test]
		[TestCaseSource(nameof(OperatorPrimitiveTestData))]
		public void EqualOperatorPrimitiveShouldReturnExpectedValue(CustomerId first, CustomerId second, bool expected)
		{
			bool result = first == second;
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(OperatorTestData))]
		public void EqualOperatorShouldReturnExpectedValue(CustomerId first, CustomerId second, bool expected)
		{
			bool result = first == second;
			result.Should().Be(expected);
		}

		[Test]
		public void ShouldNotAllowNullValue()
		{
			Action action = () => new CustomerId(null);
			action.Should().Throw<ArgumentNullException>();
		}
	}
}
