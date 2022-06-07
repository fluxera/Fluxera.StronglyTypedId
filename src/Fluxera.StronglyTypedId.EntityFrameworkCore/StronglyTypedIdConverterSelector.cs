namespace Fluxera.StronglyTypedId.EntityFrameworkCore
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	/// <inheritdoc />
	[PublicAPI]
	public class StronglyTypedIdConverterSelector : ValueConverterSelector
	{
		private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> converters
			= new ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo>();

		/// <inheritdoc />
		public StronglyTypedIdConverterSelector(ValueConverterSelectorDependencies dependencies)
			: base(dependencies)
		{
		}

		/// <inheritdoc />
		public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type providerClrType = null)
		{
			foreach(ValueConverterInfo converter in base.Select(modelClrType, providerClrType))
			{
				yield return converter;
			}

			Type underlyingModelType = modelClrType.UnwrapNullableType();
			Type underlyingProviderType = providerClrType?.UnwrapNullableType();

			if(underlyingProviderType is null || underlyingProviderType.IsStronglyTypedId())
			{
				Type valueType = underlyingModelType.GetStronglyTypedIdValueType();

				Type converterTypeTemplate = typeof(StronglyTypedIdConverter<,>);
				Type converterType = converterTypeTemplate.MakeGenericType(underlyingModelType, valueType);

				ValueConverterInfo valueConverterInfo = this.converters.GetOrAdd(
					(underlyingModelType, valueType),
					_ =>
					{
						// Create an instance of the converter whenever it's requested.
						// Build the info for our strongly-typed ID converter.
						return new ValueConverterInfo(modelClrType, valueType,
							_ => (ValueConverter)Activator.CreateInstance(converterType));
					}
				);

				yield return valueConverterInfo;
			}
		}
	}
}
