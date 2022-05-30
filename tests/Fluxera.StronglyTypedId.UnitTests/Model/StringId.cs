namespace Fluxera.StronglyTypedId.UnitTests.Model
{
	public class StringId : StronglyTypedId<StringId, string>
	{
		/// <inheritdoc />
		public StringId(string value) : base(value)
		{
		}
	}
}
