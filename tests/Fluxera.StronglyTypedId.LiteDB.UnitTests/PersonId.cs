namespace Fluxera.StronglyTypedId.LiteDB.UnitTests
{
	public sealed class PersonId : StronglyTypedId<PersonId, string>
	{
		/// <inheritdoc />
		public PersonId(string value) : base(value)
		{
		}
	}
}
