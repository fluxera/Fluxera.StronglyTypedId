namespace Fluxera.StronglyTypedId.UnitTests
{
	using System;
	using System.ComponentModel;
	using FluentAssertions;
	using Fluxera.StronglyTypedId.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class TypeConverterTests
	{
		[Test]
		public void ShouldConvertFromGuidStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(GuidId));

			GuidId result = (GuidId)converter.ConvertFromString("4c3668ce-3aaa-4adb-bece-05baa708e20f");
			result.Should().NotBeNull();
			result.Value.Should().NotBeEmpty().And.Be("4c3668ce-3aaa-4adb-bece-05baa708e20f");
		}

		[Test]
		public void ShouldConvertFromGuidValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(GuidId));

			GuidId result = (GuidId)converter.ConvertFrom(Guid.Parse("4c3668ce-3aaa-4adb-bece-05baa708e20f"));
			result.Should().NotBeNull();
			result.Value.Should().NotBeEmpty().And.Be(Guid.Parse("4c3668ce-3aaa-4adb-bece-05baa708e20f"));
		}

		[Test]
		public void ShouldConvertFromIntegerStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(IntegerId));

			IntegerId result = (IntegerId)converter.ConvertFromString("999");
			result.Should().NotBeNull();
			result.Value.Should().Be(999);
		}

		[Test]
		public void ShouldConvertFromIntegerValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(IntegerId));

			IntegerId result = (IntegerId)converter.ConvertFrom(999);
			result.Should().NotBeNull();
			result.Value.Should().Be(999);
		}

		[Test]
		public void ShouldConvertFromStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(StringId));

			StringId result = (StringId)converter.ConvertFromString("12345");
			result.Should().NotBeNull();
			result.Value.Should().NotBeEmpty().And.Be("12345");
		}

		[Test]
		public void ShouldConvertToGuidStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(GuidId));

			GuidId id = new GuidId(Guid.Parse("2ca3459d-794e-4d25-9594-bc3849972e1f"));
			string result = converter.ConvertToString(id);
			result.Should().Be("2ca3459d-794e-4d25-9594-bc3849972e1f");
		}

		[Test]
		public void ShouldConvertToGuidValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(GuidId));

			GuidId id = new GuidId(Guid.Parse("2ca3459d-794e-4d25-9594-bc3849972e1f"));
			Guid result = (Guid)converter.ConvertTo(null, null, id, typeof(Guid));
			result.Should().Be(Guid.Parse("2ca3459d-794e-4d25-9594-bc3849972e1f"));
		}

		[Test]
		public void ShouldConvertToIntegerStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(IntegerId));

			IntegerId id = new IntegerId(999);
			string result = converter.ConvertToString(id);
			result.Should().Be("999");
		}

		[Test]
		public void ShouldConvertToIntegerValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(IntegerId));

			IntegerId id = new IntegerId(999);
			int result = (int)converter.ConvertTo(null, null, id, typeof(int));
			result.Should().Be(999);
		}

		[Test]
		public void ShouldConvertToStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(StringId));

			StringId id = new StringId("12345");
			string result = converter.ConvertToString(id);
			result.Should().Be("12345");
		}
	}
}
