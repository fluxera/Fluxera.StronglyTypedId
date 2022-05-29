namespace Fluxera.StronglyTypedId
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for strongly-typed IDs.
	/// </summary>
	/// <typeparam name="TStronglyTypedId"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public interface IStronglyTypedId<TStronglyTypedId, out TKey> : IComparable<TStronglyTypedId>, IEquatable<TStronglyTypedId>
	{
		/// <summary>
		///     Gets the underlying value of the strongly-typed ID.
		/// </summary>
		public TKey Value { get; }
	}
}
