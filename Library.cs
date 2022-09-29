namespace csharp_biblioteca;

public class Library
{
    public List<Item> Items { get; } = new();
    public List<ItemBorrowLog> ItemBorrowLogs { get; } = new();
    public List<ItemReturnLog> ItemReturnLogs { get; } = new();

    public void Add(Item item)
    {
        if (Items.Contains(item))
            throw new ArgumentException("This item is already in this library.");
        
        Items.Add(item);
    }

    public void Borrow(Item item)
    {
        if (!Items.Contains(item))
            throw new ArgumentException("This item is not in this library.");
        
        if (item.State == ItemStates.Borrowed)
            throw new InvalidOperationException("This item is already borrowed.");

        item.State = ItemStates.Borrowed;
    }

    public void Return(Item item)
    {
        if (!Items.Contains(item))
            throw new ArgumentException("This item is not in this library.");
        
        item.State = ItemStates.Available;
    }
    
    public Item FindByCode(string code)
    {
        Item? item = Items.Find(x => x.Code == code);

        if (item is null)
            throw new InvalidOperationException("Impossible to find this item in the library.");
        
        return item;
    }

    public List<Item> FindByTitle(string title)
    {
        List<Item> items = Items.FindAll(x => x.Title.Contains(title));
        return items;
    }
}