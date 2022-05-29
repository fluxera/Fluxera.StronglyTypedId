namespace Fluxera.StronglyTypedId.MongoDB
{
	using System;
	using System.Reflection;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using JetBrains.Annotations;

	/// <summary>
	///     A serializer that handles instances of strongly-typed IDs.
	/// </summary>
	/// <typeparam name="TStronglyTypedId"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[PublicAPI]
	public sealed class StronglyTypedId<TStronglyTypedId, TValue> : SerializerBase<TStronglyTypedId>
		where TStronglyTypedId : StronglyTypedId.StronglyTypedId<TStronglyTypedId, TValue>
		where TValue : IComparable
	{
		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TStronglyTypedId value)
		{
			if(value is null)
			{
				context.Writer.WriteNull();
			}
			else
			{
				BsonSerializer.Serialize(context.Writer, value.Value);
			}
		}

		/// <inheritdoc />
		public override TStronglyTypedId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			if(context.Reader.CurrentBsonType == BsonType.Null)
			{
				context.Reader.ReadNull();
				return null;
			}

			TValue value = BsonSerializer.Deserialize<TValue>(context.Reader);
			object instance = Activator.CreateInstance(args.NominalType, BindingFlags.Public | BindingFlags.Instance, null, new object[] { value }, null);
			return (TStronglyTypedId)instance;
		}
	}
}
