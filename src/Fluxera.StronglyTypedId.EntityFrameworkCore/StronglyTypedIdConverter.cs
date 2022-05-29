namespace Fluxera.StronglyTypedId.EntityFrameworkCore
{
	using System;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class StronglyTypedIdConverter<TStronglyTypedId, TValue> : ValueConverter<TStronglyTypedId, TValue>
		where TStronglyTypedId : StronglyTypedId<TStronglyTypedId, TValue>
		where TValue : IComparable
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="StronglyTypedIdConverter{TStronglyTypedId,TValue}" /> type.
		/// </summary>
		public StronglyTypedIdConverter()
			: base(valueObject => Serialize(valueObject), value => Deserialize(value))
		{
		}

		private static TValue Serialize(TStronglyTypedId valueObject)
		{
			TValue value = valueObject.Value;
			return value;
		}

		private static TStronglyTypedId Deserialize(TValue value)
		{
			if(value is null)
			{
				return null;
			}

			object instance = Activator.CreateInstance(typeof(TStronglyTypedId), BindingFlags.Public | BindingFlags.Instance, null, new object[] { value }, null);
			return (TStronglyTypedId)instance;
		}
	}
}
