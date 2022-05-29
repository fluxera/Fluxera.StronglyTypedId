namespace Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests
{
	public sealed class PersonId : StronglyTypedId<PersonId, string>
	{
		/// <inheritdoc />
		public PersonId(string value) : base(value)
		{
		}
	}
}
