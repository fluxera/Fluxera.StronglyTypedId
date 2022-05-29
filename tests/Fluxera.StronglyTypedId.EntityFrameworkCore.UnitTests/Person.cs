namespace Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests
{
	using System.ComponentModel.DataAnnotations;

	public class Person
	{
		[Key]
		public string Id { get; set; }

		public PersonId PersonId { get; set; }
	}
}
