namespace Fluxera.StronglyTypedId.MongoDB.UnitTests
{
	public sealed class PersonId : StronglyTypedId.StronglyTypedId<PersonId, string>
	{
		/// <inheritdoc />
		public PersonId(string value) : base(value)
		{
		}
	}
}
