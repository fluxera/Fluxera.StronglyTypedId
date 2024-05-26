namespace Fluxera.StronglyTypedId
{
	using System;

	internal static class TypeExtensions
	{
		/// <summary>
		///     Determines if the given type is numeric.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///     <c>true</c> if the specified type is numeric; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNumeric(this Type type)
		{
			type = type.UnwrapNullableType();
			return
				(type == typeof(sbyte)) ||
				(type == typeof(byte)) ||
				(type == typeof(short)) ||
				(type == typeof(ushort)) ||
				(type == typeof(int)) ||
				(type == typeof(uint)) ||
				(type == typeof(long)) ||
				(type == typeof(ulong)) ||
				(type == typeof(decimal)) ||
				(type == typeof(float)) ||
				(type == typeof(double));
		}

		/// <summary>
		///     Gets the type without nullable if the type is a <see cref="Nullable{T}" />.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static Type UnwrapNullableType(this Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}
	}
}
