namespace Fluxera.StronglyTypedId.MongoDB
{
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="ConventionPack" /> type.
	/// </summary>
	[PublicAPI]
	public static class ConventionPackExtensions
	{
		/// <summary>
		///     Configure the serializer to use the <see cref="StronglyTypedIdSerializer{TStronglyTypedId,TValue}" />.
		/// </summary>
		/// <param name="pack"></param>
		/// <param name="stringRepresentation"></param>
		/// <param name="guidRepresentation"></param>
		/// <returns></returns>
		public static ConventionPack UseStronglyTypedId(this ConventionPack pack,
			BsonType stringRepresentation = BsonType.ObjectId,
			GuidRepresentation guidRepresentation = GuidRepresentation.Standard)
		{
			pack.Add(new StronglyTypedIdConvention(stringRepresentation, guidRepresentation));

			return pack;
		}
	}
}
