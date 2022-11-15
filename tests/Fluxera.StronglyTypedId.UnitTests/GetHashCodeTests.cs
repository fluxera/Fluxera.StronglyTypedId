// ReSharper disable InconsistentNaming

namespace Fluxera.StronglyTypedId.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class GetHashCodeTests
	{
		private static IEnumerable<object[]> TestData = new List<object[]>
		{
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
		};

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void GetHashCodeShouldReturnExpectedValue(object first, object second, bool expected)
		{
			Console.WriteLine($"{first.GetHashCode()} : {second.GetHashCode()}");

			bool result = first.GetHashCode() == second.GetHashCode();
			result.Should().Be(expected);
		}
	}
}
