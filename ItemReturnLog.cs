namespace csharp_biblioteca;

public class ItemReturnLog : ItemLog
{
    public ItemReturnLog(User owner, Item item, DateTime date)
        : base(owner, item, date)
    {
    }
}