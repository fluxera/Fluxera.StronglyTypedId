// ReSharper disable InconsistentNaming
// ReSharper disable ObjectCreationAsStatement

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
				null,
				null,
				true
			},
			new object[]
			{
				new StringId("12345"),
				new StringId("12345"),
				true
			},
			new object[]
			{
				new StringId("12345"),
				new StringId("12346"),
				false
			},
			new object[]
			{
				new StringId("12345"),
				null,
				false
			}
		};

		private static IEnumerable<object[]> OperatorPrimitiveTestData = new List<object[]>
		{
			new object[]
			{
				new StringId("12345"),
				new StringId("12345"),
				true
			}
		};

		[Test]
		[TestCaseSource(nameof(OperatorPrimitiveTestData))]
		public void EqualOperatorPrimitiveShouldReturnExpectedValue(StringId first, StringId second, bool expected)
		{
			bool result = first == second;
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(OperatorTestData))]
		public void EqualOperatorShouldReturnExpectedValue(StringId first, StringId second, bool expected)
		{
			bool result = first == second;
			result.Should().Be(expected);
		}

		[Test]
		public void ShouldNotAllowNullValue()
		{
			Action action = () => new StringId(null);
			action.Should().Throw<ArgumentNullException>();
		}
	}
}
