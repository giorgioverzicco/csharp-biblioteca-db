namespace csharp_biblioteca;

public class Author
{
    public string FirstName { get; }
    public string LastName { get; }

    public Author(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}