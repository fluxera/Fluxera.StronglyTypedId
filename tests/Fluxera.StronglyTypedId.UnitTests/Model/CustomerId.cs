namespace Fluxera.StronglyTypedId.UnitTests.Model
{
	public class CustomerId : StronglyTypedId<CustomerId, string>
	{
		/// <inheritdoc />
		public CustomerId(string value) : base(value)
		{
		}
	}
}
