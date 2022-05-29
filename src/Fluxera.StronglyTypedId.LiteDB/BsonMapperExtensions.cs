namespace Fluxera.StronglyTypedId.LiteDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Guards;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="BsonMapper" /> type.
	/// </summary>
	[PublicAPI]
	public static class BsonMapperExtensions
	{
		/// <summary>
		///     Configure the mapper to use the <see cref="StronglyTypedIdConverter" />.
		/// </summary>
		/// <param name="mapper"></param>
		/// <returns></returns>
		public static BsonMapper UseStronglyTypedId(this BsonMapper mapper)
		{
			Guard.Against.Null(mapper);

			IEnumerable<Type> stronglyTypedIdTypes = AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(x => x.GetTypes())
				.Where(x => x.IsStronglyTypedId());

			foreach(Type stronglyTypedIdType in stronglyTypedIdTypes)
			{
				mapper.RegisterType(stronglyTypedIdType,
					StronglyTypedIdConverter.Serialize(stronglyTypedIdType),
					StronglyTypedIdConverter.Deserialize(stronglyTypedIdType));
			}

			return mapper;
		}
	}
}
