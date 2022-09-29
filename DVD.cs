namespace csharp_biblioteca;

public class DVD : Item
{
    public int Duration { get; }

    public DVD(string title, int year, int duration)
        : base(title, year)
    {
        Duration = duration;
    }
    
    public DVD(string title, int year, int duration, string category, string shelf, Author author) 
        : base(title, year, category, shelf, author)
    {
        Duration = duration;
    }
    
    public override string ToString()
    {
        return
            base.ToString() +
            $"Duration: {Duration} minutes\n";
    }
}