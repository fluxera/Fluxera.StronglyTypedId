namespace Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests.Model
{
	using Fluxera.StronglyTypedId;

	public sealed class PersonId : StronglyTypedId<PersonId, string>
	{
		/// <inheritdoc />
		public PersonId(string value) : base(value)
		{
		}
	}
}
