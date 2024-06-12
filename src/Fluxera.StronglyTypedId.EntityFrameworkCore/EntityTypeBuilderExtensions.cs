namespace Fluxera.StronglyTypedId.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	/// <summary>
	///     Extension methods for the <see cref="ModelBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class EntityTypeBuilderExtensions
	{
		/// <summary>
		///     Configure the <see cref="EntityTypeBuilder" /> to use the
		///     <see cref="StronglyTypedIdConverter{TStronglyTypedId,TValue}" />.
		/// </summary>
		/// <param name="entityTypeBuilder"></param>
		public static void UseStronglyTypedId(this EntityTypeBuilder entityTypeBuilder)
		{
			Guard.ThrowIfNull(entityTypeBuilder);

			IEnumerable<PropertyInfo> properties = entityTypeBuilder.Metadata
				.ClrType
				.GetProperties()
				.Where(propertyInfo => propertyInfo.PropertyType.UnwrapNullableType().IsStronglyTypedId());

			foreach(PropertyInfo property in properties)
			{
				Type originalMemberType = property.PropertyType;
				Type memberType = Nullable.GetUnderlyingType(originalMemberType) ?? originalMemberType;

				if(memberType.IsStronglyTypedId())
				{
					Type valueType = memberType.GetStronglyTypedIdValueType();

					Type converterTypeTemplate = typeof(StronglyTypedIdConverter<,>);
					Type converterType = converterTypeTemplate.MakeGenericType(memberType, valueType);

					entityTypeBuilder
						.Property(property.Name)
						.HasConversion(converterType);
				}
			}
		}
	}
}
