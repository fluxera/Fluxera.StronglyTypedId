namespace Fluxera.StronglyTypedId.JsonNet.UnitTests
{
	public sealed class PersonId : StronglyTypedId<PersonId, string>
	{
		/// <inheritdoc />
		public PersonId(string value) : base(value)
		{
		}
	}
}
