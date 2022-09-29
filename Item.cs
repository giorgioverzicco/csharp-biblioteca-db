namespace csharp_biblioteca;

public class Item
{
    public string Code { get; }
    public string Title { get; } = String.Empty;
    public int Year { get; }
    public string Category { get; } = String.Empty;
    public ItemStates State { get; set; }  = ItemStates.Available;
    public string Shelf { get; } = String.Empty;
    public Author Author { get; }

    public Item()
    {
        Code = new Random().Next(1, 1000).ToString("D13");
        Title = Code + " - Empty";
        Author = new Author("Anonymous", "Anonymous");
    }

    public Item(string title, int year)
        : this()
    {
        Title = title;
        Year = year;
    }
    
    public Item(string title, int year, Author author)
        : this(title, year)
    {
        Author = author;
    }

    public Item(
        string title, 
        int year, 
        string category, 
        string shelf, 
        Author author)
        : this(title, year, author)
    {
        Category = category;
        Shelf = shelf;
    }

    public override int GetHashCode() => Code.GetHashCode();
    
    public override string ToString()
    {
        return
            $"Code: {Code}\n" +
            $"Title: {Title}\n" +
            $"Year: {Year}\n" +
            $"State: {State}\n";
    }
}