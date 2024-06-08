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
	public interface IStronglyTypedId<TStronglyTypedId, TKey> : IComparable<TStronglyTypedId>, IEquatable<TStronglyTypedId>
		where TKey : notnull, IComparable
	{
		/// <summary>
		///     Gets the underlying value of the strongly-typed ID.
		/// </summary>
		public TKey Value { get; }

#if NET7_0_OR_GREATER
		/// <summary>
		///		Parses the <see cref="TStronglyTypedId"/> from the given string value.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		static abstract bool TryParse(string value, out TStronglyTypedId id);

		/// <summary>
		///		Creates a new instance of the <see cref="TStronglyTypedId"/> with the given value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static abstract TStronglyTypedId Create(TKey value);
#endif
	}
}
