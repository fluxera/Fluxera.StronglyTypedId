namespace Fluxera.StronglyTypedId.MongoDB
{
	using System;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using global::MongoDB.Bson.Serialization.Serializers;
	using JetBrains.Annotations;

	/// <summary>
	///     A convention that enables support for serializing strongly-typed IDs.
	/// </summary>
	[PublicAPI]
	public sealed class StronglyTypedIdConvention : ConventionBase, IMemberMapConvention
	{
		private readonly IBsonSerializer guidSerializer;
		private readonly IBsonSerializer stringSerializer;

		/// <summary>
		///     Initializes a new instance of the <see cref="StronglyTypedIdConvention" /> type.
		/// </summary>
		/// <param name="stringRepresentation"></param>
		/// <param name="guidRepresentation"></param>
		public StronglyTypedIdConvention(
			BsonType stringRepresentation = BsonType.ObjectId,
			GuidRepresentation guidRepresentation = GuidRepresentation.Standard)
		{
			this.stringSerializer = new StringSerializer(stringRepresentation);
			this.guidSerializer = new GuidSerializer(guidRepresentation);
		}

		/// <inheritdoc />
		public void Apply(BsonMemberMap memberMap)
		{
			Type originalMemberType = memberMap.MemberType;
			Type memberType = originalMemberType.UnwrapNullableType();

			if(memberType.IsStronglyTypedId())
			{
				Type valueType = memberType.GetStronglyTypedIdValueType();
				Type serializerTypeTemplate = typeof(StronglyTypedIdSerializer<,>);
				Type serializerType = serializerTypeTemplate.MakeGenericType(memberType, valueType);

				IBsonSerializer serializer;

				if(valueType == typeof(string))
				{
					serializer = this.stringSerializer;
				}
				else if(valueType == typeof(Guid))
				{
					serializer = this.guidSerializer;
				}
				else
				{
					serializer = BsonSerializer.LookupSerializer(valueType);
				}

				IBsonSerializer enumerationSerializer = (IBsonSerializer)Activator.CreateInstance(serializerType, [serializer]);
				memberMap.SetSerializer(enumerationSerializer);
			}
		}
	}
}
