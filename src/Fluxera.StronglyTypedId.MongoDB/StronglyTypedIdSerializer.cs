namespace Fluxera.StronglyTypedId.MongoDB
{
	using System;
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
	public sealed class StronglyTypedIdSerializer<TStronglyTypedId, TValue> : SerializerBase<TStronglyTypedId>
		where TStronglyTypedId : StronglyTypedId<TStronglyTypedId, TValue>
		where TValue : IComparable
	{
		private readonly IBsonSerializer idValueSerializer;

		/// <summary>
		///     Initializes a new instance of the <see cref="StronglyTypedIdSerializer{TStronglyTypedId,TValue}" />;
		/// </summary>
		/// <param name="idValueSerializer"></param>
		public StronglyTypedIdSerializer(IBsonSerializer idValueSerializer)
		{
			this.idValueSerializer = idValueSerializer;
		}

		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TStronglyTypedId value)
		{
			if(value is null)
			{
				context.Writer.WriteNull();
			}
			else
			{
				if(this.idValueSerializer != null)
				{
					this.idValueSerializer.Serialize(context, args, value.Value);
				}
				else
				{
					BsonSerializer.Serialize(context.Writer, value.Value);
				}
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

			TValue value;

			if(this.idValueSerializer != null)
			{
				value = (TValue)this.idValueSerializer.Deserialize(context, args);
			}
			else
			{
				value = BsonSerializer.Deserialize<TValue>(context.Reader);
			}

			object instance = Activator.CreateInstance(args.NominalType, new object[] { value });
			return (TStronglyTypedId)instance;
		}
	}
}
