namespace Fluxera.StronglyTypedId.UnitTests.Model
{
	using System;

	public class GuidId : StronglyTypedId<GuidId, Guid>
	{
		/// <inheritdoc />
		public GuidId(Guid value) : base(value)
		{
		}
	}
}
