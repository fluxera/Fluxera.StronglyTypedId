namespace Fluxera.StronglyTypedId
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for any strongly-typed ID.
	/// </summary>
	/// <typeparam name="TStronglyTypedId">The type of the strongly-typed ID.</typeparam>
	/// <typeparam name="TValue">The type of the IDs value.</typeparam>
	[PublicAPI]
	[TypeConverter(typeof(StronglyTypedIdConverter))]
	public abstract class StronglyTypedId<TStronglyTypedId, TValue> : IStronglyTypedId<TStronglyTypedId, TValue>
		where TStronglyTypedId : StronglyTypedId<TStronglyTypedId, TValue>
		where TValue : notnull, IComparable
	{
		/// <summary>
		///     To ensure hashcode uniqueness, a carefully selected random number multiplier
		///     is used within the calculation.
		/// </summary>
		/// <remarks>
		///     See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// </remarks>
		private const int HashMultiplier = 37;

		static StronglyTypedId()
		{
			Type valueType = typeof(TValue);
			bool isIdentifierType = valueType.IsNumeric() || valueType == typeof(string) || valueType == typeof(Guid);

			Guard.Against.False(isIdentifierType, nameof(Value), "The value of a strongly-typed ID must be a numeric, string or Guid type.");
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="StronglyTypedId{TStronglyTypedId,TValue}" /> type.
		/// </summary>
		/// <param name="value"></param>
		protected StronglyTypedId(TValue value)
		{
			this.Value = Guard.Against.Null(value);
		}

		/// <summary>
		///     Gets or sets value if the strongly-typed ID.
		/// </summary>
		public TValue Value { get; private set; }

		/// <inheritdoc />
		public bool Equals(TStronglyTypedId other)
		{
			return this.Equals(other as object);
		}

		/// <inheritdoc />
		public int CompareTo(TStronglyTypedId other)
		{
			return (this.Value, other.Value) switch
			{
				(null, null) => 0,
				(null, _) => -1,
				(_, null) => 1,
				(_, _) => this.Value.CompareTo(other.Value)
			};
		}

		/// <summary>
		///     Checks if the given IDs are equal.
		/// </summary>
		public static bool operator ==(StronglyTypedId<TStronglyTypedId, TValue> left, StronglyTypedId<TStronglyTypedId, TValue> right)
		{
			if(left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		/// <summary>
		///     Checks if the given IDs are not equal.
		/// </summary>
		public static bool operator !=(StronglyTypedId<TStronglyTypedId, TValue> left, StronglyTypedId<TStronglyTypedId, TValue> right)
		{
			return !(left == right);
		}

		/// <summary>
		///     Converts a value implicitly to an instance of TStronglyTypedId.
		/// </summary>
		/// <param name="value"></param>
		public static explicit operator StronglyTypedId<TStronglyTypedId, TValue>(TValue value)
		{
			object instance = Activator.CreateInstance(typeof(TStronglyTypedId), new object[] { value });
			return (TStronglyTypedId)instance;
		}

		/// <inheritdoc />
		public sealed override bool Equals(object obj)
		{
			if(obj is null)
			{
				return false;
			}

			if(object.ReferenceEquals(this, obj))
			{
				return true;
			}

			StronglyTypedId<TStronglyTypedId, TValue> other = obj as StronglyTypedId<TStronglyTypedId, TValue>;
			return other != null
				&& this.GetType() == other.GetType()
				&& this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
		}

		/// <inheritdoc />
		public sealed override int GetHashCode()
		{
			unchecked
			{
				// It is possible for two objects to return the same hash code based on
				// identically valued properties, even if they are of different types,
				// so we include the value object type in the hash calculation.
				int hashCode = this.GetType().GetHashCode();

				foreach(object component in this.GetEqualityComponents())
				{
					if(component != null)
					{
						hashCode = hashCode * HashMultiplier ^ component.GetHashCode();
					}
				}

				return hashCode;
			}
		}

		/// <inheritdoc />
		public sealed override string ToString()
		{
			return this.Value.ToString();
		}

		/// <summary>
		///     Gets all components of the value object that are used for equality. <br />
		///     The default implementation get all properties via reflection. One
		///     can at any time override this behavior with a manual or custom implementation.
		/// </summary>
		/// <returns>The components to use for equality.</returns>
		private IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Value;
		}
	}
}
