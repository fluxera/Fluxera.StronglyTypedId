namespace Fluxera.StronglyTypedId.SystemTextJson
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class StronglyTypedIdConverter<TStronglyTypedId, TValue> : JsonConverter<TStronglyTypedId>
		where TStronglyTypedId : StronglyTypedId<TStronglyTypedId, TValue>
		where TValue : IComparable
	{
		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, TStronglyTypedId value, JsonSerializerOptions options)
		{
			if(value is null)
			{
				writer.WriteNullValue();
			}
			else
			{
				JsonSerializer.Serialize(writer, value.Value, options);
			}
		}

		/// <inheritdoc />
		public override TStronglyTypedId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}

			TValue value = JsonSerializer.Deserialize<TValue>(ref reader, options);
			object instance = Activator.CreateInstance(typeToConvert, [value]);
			return (TStronglyTypedId)instance;
		}
	}
}
