namespace Fluxera.StronglyTypedId.JsonNet
{
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	/// <summary>
	///     Extension methods for the <see cref="JsonSerializerSettings" /> type.
	/// </summary>
	[PublicAPI]
	public static class JsonSerializerSettingsExtensions
	{
		/// <summary>
		///     Configure the serializer to use the <see cref="StronglyTypedIdConverter{TStronglyTypedId,TValue}" />.
		/// </summary>
		/// <param name="settings"></param>
		public static void UseStronglyTypedId(this JsonSerializerSettings settings)
		{
			settings.ContractResolver = new CompositeContractResolver
			{
				new StronglyTypedIdContractResolver()
			};
		}
	}
}
