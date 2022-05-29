namespace Fluxera.StronglyTypedId.MongoDB
{
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="ConventionPack" /> type.
	/// </summary>
	[PublicAPI]
	public static class ConventionPackExtensions
	{
		/// <summary>
		///     Configure the serializer to use the <see cref="StronglyTypedId{TStronglyTypedId,TValue}" />.
		/// </summary>
		/// <param name="pack"></param>
		/// <returns></returns>
		public static ConventionPack UseStronglyTypedId(this ConventionPack pack)
		{
			pack.Add(new StronglyTypedIdConvention());

			return pack;
		}
	}
}
