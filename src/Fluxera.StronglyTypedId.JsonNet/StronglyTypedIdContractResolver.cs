namespace Fluxera.StronglyTypedId.JsonNet
{
	using System;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class StronglyTypedIdContractResolver : DefaultContractResolver
	{
		/// <inheritdoc />
		protected override JsonConverter ResolveContractConverter(Type objectType)
		{
			if(objectType.IsStronglyTypedId())
			{
				Type valueType = objectType.GetStronglyTypedIdValueType();
				Type converterTypeTemplate = typeof(StronglyTypedIdConverter<,>);
				Type converterType = converterTypeTemplate.MakeGenericType(objectType, valueType);

				return (JsonConverter)Activator.CreateInstance(converterType);
			}

			return base.ResolveContractConverter(objectType);
		}
	}
}
