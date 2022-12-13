namespace Fluxera.StronglyTypedId.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
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
			Guard.Against.Null(entityTypeBuilder);

			IEnumerable<PropertyInfo> properties = entityTypeBuilder.Metadata
				.ClrType
				.GetProperties()
				.Where(propertyInfo => propertyInfo.PropertyType.UnwrapNullableType().IsStronglyTypedId());

			foreach(PropertyInfo property in properties)
			{
				Type idType = property.PropertyType;
				Type valueType = idType.GetStronglyTypedIdValueType();

				Type converterTypeTemplate = typeof(StronglyTypedIdConverter<,>);
				Type converterType = converterTypeTemplate.MakeGenericType(idType, valueType);

				entityTypeBuilder
					.Property(property.Name)
					.HasConversion(converterType);
			}
		}
	}
}
