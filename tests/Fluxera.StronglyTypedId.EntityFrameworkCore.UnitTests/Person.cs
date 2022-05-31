namespace Fluxera.StronglyTypedId.EntityFrameworkCore.UnitTests
{
	using System.ComponentModel.DataAnnotations;

	public class Person
	{
		[Key]
		public PersonId Id { get; set; }

		public string Name { get; set; }
	}
}
