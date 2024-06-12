namespace Fluxera.StronglyTypedId.LiteDB
{
	using System;
	using System.Reflection;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     A converter for primitive value objects.
	/// </summary>
	[PublicAPI]
	public static class StronglyTypedIdConverter
	{
		/// <summary>
		///     Serialize the given ID instance.
		/// </summary>
		/// <param name="stronglyTypedIdType"></param>
		/// <returns></returns>
		public static Func<object, BsonValue> Serialize(Type stronglyTypedIdType)
		{
			return obj =>
			{
				PropertyInfo property = stronglyTypedIdType.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
				object value = property?.GetValue(obj);

				BsonValue bsonValue = new BsonValue(value);
				return bsonValue;
			};
		}

		/// <summary>
		///     Deserialize a ID instance from the given bson value.
		/// </summary>
		/// <param name="stronglyTypedIdType"></param>
		/// <returns></returns>
		public static Func<BsonValue, object> Deserialize(Type stronglyTypedIdType)
		{
			return bson =>
			{
				if(bson.IsNull)
				{
					return null;
				}

				object value = bson.RawValue;
				object instance = Activator.CreateInstance(stronglyTypedIdType, [value]);
				return instance;
			};
		}
	}
}
