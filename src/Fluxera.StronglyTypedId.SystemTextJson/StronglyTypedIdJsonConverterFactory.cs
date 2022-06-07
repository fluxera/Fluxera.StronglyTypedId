namespace Fluxera.StronglyTypedId.SystemTextJson
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class StronglyTypedIdJsonConverterFactory : JsonConverterFactory
	{
		/// <inheritdoc />
		public override bool CanConvert(Type typeToConvert)
		{
			bool isPrimitiveValueObject = typeToConvert.IsStronglyTypedId();
			return isPrimitiveValueObject;
		}

		/// <inheritdoc />
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			Type valueType = typeToConvert.GetStronglyTypedIdValueType();
			Type converterTypeTemplate = typeof(StronglyTypedIdConverter<,>);
			Type converterType = converterTypeTemplate.MakeGenericType(typeToConvert, valueType);

			return (JsonConverter)Activator.CreateInstance(converterType);
		}
	}
}
