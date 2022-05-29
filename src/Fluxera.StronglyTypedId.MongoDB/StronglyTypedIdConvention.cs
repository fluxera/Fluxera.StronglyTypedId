namespace Fluxera.StronglyTypedId.MongoDB
{
	using System;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	/// <summary>
	///     A convention that enables support for serializing strongly-typed IDs.
	/// </summary>
	[PublicAPI]
	public sealed class StronglyTypedIdConvention : ConventionBase, IMemberMapConvention
	{
		/// <inheritdoc />
		public void Apply(BsonMemberMap memberMap)
		{
			Type originalMemberType = memberMap.MemberType;
			Type memberType = Nullable.GetUnderlyingType(originalMemberType) ?? originalMemberType;

			if(memberType.IsStronglyTypedId())
			{
				Type valueType = memberType.GetValueType();
				Type serializerTypeTemplate = typeof(StronglyTypedId<,>);
				Type serializerType = serializerTypeTemplate.MakeGenericType(memberType, valueType);

				IBsonSerializer enumerationSerializer = (IBsonSerializer)Activator.CreateInstance(serializerType);
				memberMap.SetSerializer(enumerationSerializer);
			}
		}
	}
}
