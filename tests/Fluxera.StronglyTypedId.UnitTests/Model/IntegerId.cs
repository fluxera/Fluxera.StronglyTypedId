namespace Fluxera.StronglyTypedId.UnitTests.Model
{
	public class IntegerId : StronglyTypedId<IntegerId, int>
	{
		/// <inheritdoc />
		public IntegerId(int value) : base(value)
		{
		}
	}
}
