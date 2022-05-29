# Fluxera.StronglyTypedId

A library that provides strongly-typed Ids without code generation. 


## Usage

To implement a strongly-typed ID just inherit from the ```StronglyTypedId<,>``` base class.
The base class implements all aspects of the ID, like quality and comparablility.

```C#
public sealed class PersonId : StronglyTypedId<PersonId, string>
{
	/// <inheritdoc />
	public PersonId(string value) : base(value)
	{
	}
}
```

## Serializer Support

This library provides serializer support for the following libaries:

- EF Core
- JSON.NET
- LiteDB
- MongoDB
- System.Text.Json

To use the serializer support just use the ```UseStronglyTypedId``` extension method to configure the
corresponding serializer.
