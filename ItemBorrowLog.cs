namespace csharp_biblioteca;

public class ItemBorrowLog : ItemLog
{
    public ItemBorrowLog(User owner, Item item, DateTime date)
        : base(owner, item, date)
    {
    }
}