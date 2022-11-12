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
	using Microsoft.EntityFrameworkCore.Metadata;

	/// <summary>
	///     Extension methods for the <see cref="ModelBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class ModelBuilderExtensions
	{
		/// <summary>
		///     Configure the model builder to use the <see cref="StronglyTypedIdConverter{TStronglyTypedId,TValue}" />.
		/// </summary>
		/// <param name="modelBuilder"></param>
		public static void UseStronglyTypedId(this ModelBuilder modelBuilder)
		{
			Guard.Against.Null(modelBuilder);

			foreach(IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
			{
				IEnumerable<PropertyInfo> properties = entityType
					.ClrType
					.GetProperties()
					.Where(propertyInfo => propertyInfo.PropertyType.UnwrapNullableType().IsStronglyTypedId());

				foreach(PropertyInfo property in properties)
				{
					Type idType = property.PropertyType;
					Type valueType = idType.GetStronglyTypedIdValueType();

					Type converterTypeTemplate = typeof(StronglyTypedIdConverter<,>);
					Type converterType = converterTypeTemplate.MakeGenericType(idType, valueType);

					modelBuilder
						.Entity(entityType.ClrType)
						.Property(property.Name)
						.HasConversion(converterType);
				}
			}
		}
	}
}
