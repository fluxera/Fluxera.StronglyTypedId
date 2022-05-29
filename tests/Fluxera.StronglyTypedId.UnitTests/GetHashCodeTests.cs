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
		private static IEnumerable<object[]> PrimitiveTestData = new List<object[]>
		{
			new object[]
			{
				new CustomerId("12345"),
				new CustomerId("12345"),
				true
			},
		};

		private static IEnumerable<object[]> TestData = new List<object[]>
		{
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
		};

		[Test]
		[TestCaseSource(nameof(TestData))]
		[TestCaseSource(nameof(PrimitiveTestData))]
		public void GetHashCodeShouldReturnExpectedValue(object first, object second, bool expected)
		{
			Console.WriteLine($"{first.GetHashCode()} : {second.GetHashCode()}");

			bool result = first.GetHashCode() == second.GetHashCode();
			result.Should().Be(expected);
		}
	}
}
