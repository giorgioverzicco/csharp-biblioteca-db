namespace csharp_biblioteca;

public class ItemLog
{
    public User Owner { get; }
    public Item Item { get; }
    public DateTime Date { get; }

    public ItemLog(User owner, Item item, DateTime date)
    {
        Owner = owner;
        Item = item;
        Date = date;
    }

    public override string ToString()
    {
        return $"Owner: {Owner.FirstName} {Owner.LastName} | Item: {Item.Title} | Date: {Date}";
    }
}