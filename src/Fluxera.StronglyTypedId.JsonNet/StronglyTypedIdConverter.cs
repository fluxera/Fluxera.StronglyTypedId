namespace Fluxera.StronglyTypedId.JsonNet
{
	using System;
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class StronglyTypedIdConverter<TStronglyTypedId, TValue> : JsonConverter<TStronglyTypedId>
		where TStronglyTypedId : StronglyTypedId<TStronglyTypedId, TValue>
		where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
	{
		/// <inheritdoc />
		public override bool CanWrite => true;

		/// <inheritdoc />
		public override bool CanRead => true;

		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, TStronglyTypedId value, JsonSerializer serializer)
		{
			if(value is null)
			{
				writer.WriteNull();
			}
			else
			{
				writer.WriteValue(value.Value);
			}
		}

		/// <inheritdoc />
		public override TStronglyTypedId ReadJson(JsonReader reader, Type objectType, TStronglyTypedId existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.Null)
			{
				return null;
			}

			TValue value = serializer.Deserialize<TValue>(reader);
			object instance = Activator.CreateInstance(objectType, [value]);
			return (TStronglyTypedId)instance;
		}
	}
}
