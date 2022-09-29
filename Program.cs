using csharp_biblioteca;

Library library = new Library();

library.Add(
    new Book(
        "In Search of Lost Time",
        1913,
        300));
library.Add(
    new Book(
        "Ulysses",
        1904,
        120));
library.Add(
    new Book(
        "A Lost Lady",
        1923,
        90));
library.Add(
    new DVD(
        "The Automat",
        2022,
        120));
library.Add(
    new DVD(
        "The Godfather",
        2001,
        140));

User? user = null;
int choice;

do
{
    PrintOptions();
    choice = GetUserChoice();
    Console.WriteLine();

    switch (choice)
    {
        case 1:
            user = RegisterUser();
            break;
        case 2:
            ListAllItemsInLibrary(library);
            break;
        case 3:
            FindItemByCode(library, user);
            break;
        case 4:
            FindItemByTitle(library, user);
            break;
        case 5:
            BorrowItem(library, user);
            break;
        case 6:
            ReturnItem(library, user);
            break;
        case 7:
            ListBorrowedItems(user);
            break;
        case 8:
            ListBorrowedItemsInLibrary(library);
            break;
    }
} while (choice != -1);

void ListBorrowedItemsInLibrary(Library library)
{
    if (library.ItemBorrowLogs.Count > 0)
    {
        Console.WriteLine("Here's all the borrows in the library:");
        foreach (var log in library.ItemBorrowLogs)
        {
            Console.WriteLine(log);
        }
    }
    else
    {
        Console.WriteLine("Nothing is taken from this library.");
    }
}

void ListBorrowedItems(User? user)
{
    if (!IsUserRegistered(user)) return;

    if (user.BorrowedItems.Count > 0)
    {
        Console.WriteLine("Here's the borrowed items:");
        foreach (var item in user.BorrowedItems)
        {
            Console.WriteLine(item);
        }
    }
    else
    {
        Console.WriteLine("There's no item borrowed.");
    }
}

void ReturnItem(Library library, User? user)
{
    if (!IsUserRegistered(user)) return;

    Console.WriteLine("Please, enter the code you want to search for:");
    Console.Write("> ");
    
    string code = Console.ReadLine() ?? "0000000000000";
    Item? item = user.BorrowedItems.Find(x => x.Code == code);

    try
    {
        user.Return(library, item);
        Console.WriteLine($"You have returned to the library: {item.Title}\n");
    }
    catch (ArgumentException _)
    {
        Console.WriteLine("This item does not belong to this library.");
    }
}

void BorrowItem(Library library, User? user)
{
    if (!IsUserRegistered(user)) return;

    Item? item = FindItemByCode(library, user);

    try
    {
        user.Borrow(library, item);
        Console.WriteLine($"You have borrowed from the library: {item.Title}");
    }
    catch (ArgumentException _)
    {
        Console.WriteLine("This item does not belong to this library.");
    }
    catch (InvalidOperationException _)
    {
        Console.WriteLine("This item is already borrowed.");
    }
}

void FindItemByTitle(Library library, User? user)
{
    if (!IsUserRegistered(user)) return;
    
    Console.WriteLine("Please, enter the title you want to search for:");
    Console.Write("> ");
    
    string title = Console.ReadLine() ?? "Test";
    List<Item> items = user.SearchByTitle(library, title);
    
    Console.WriteLine();

    if (items.Count > 0)
    {
        Console.WriteLine("Here's the results:");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
    else
    {
        Console.WriteLine($"There's no item with title: {title}.");
    }
}

Item? FindItemByCode(Library library, User? user)
{
    if (!IsUserRegistered(user)) return null;
    
    Console.WriteLine("Please, enter the code you want to search for:");
    Console.Write("> ");
    
    string code = Console.ReadLine() ?? "0000000000000";
    Item? item = null;

    Console.WriteLine();

    try
    {
        item = user.SearchByCode(library, code);
        Console.WriteLine("Here's the result:");
        Console.WriteLine(item);
    }
    catch (InvalidOperationException _)
    {
        Console.WriteLine($"Impossible to find an item with code: {code}.");
    }

    return item;
}

void ListAllItemsInLibrary(Library library)
{
    if (library.Items.Count > 0)
    {
        Console.WriteLine("Here's the library items:");
        foreach (var item in library.Items)
        {
            Console.WriteLine(item);
        }
    }
    else
    {
        Console.WriteLine("There's no item in the library.");
    }
}

bool IsUserRegistered(User? user)
{
    if (user is null)
    {
        Console.WriteLine("You must register your account before you can do anything.");
        return false;
    }
    
    return true;
}

User RegisterUser()
{
    Console.WriteLine("Please, enter your details:");
    Console.Write("First Name: ");
    string firstName = Console.ReadLine() ?? "DummyFirst";
    
    Console.Write("Last Name: ");
    string lastName = Console.ReadLine() ?? "DummyLast";
    
    Console.Write("Email: ");
    string email = Console.ReadLine() ?? "DummyEmail@dummy.com";
    
    Console.Write("Password: ");
    string password = Console.ReadLine() ?? "DummyPassword";
    
    Console.Write("Phone Number: ");
    string phoneNumber = Console.ReadLine() ?? "3333333333";
    
    Console.WriteLine();

    return new User(
        firstName, lastName, email, password, phoneNumber);
}

int GetUserChoice()
{
    int choice = Convert.ToInt32(Console.ReadLine());
    return choice;
}

void PrintOptions()
{
    Console.WriteLine("Select what you want to do (-1 to abort):");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. List all library items");
    Console.WriteLine("3. Find an item by his code");
    Console.WriteLine("4. Find an item by his title");
    Console.WriteLine("5. Borrow an item");
    Console.WriteLine("6. Return an item");
    Console.WriteLine("7. List all your borrowed items");
    Console.WriteLine("8. List all the borrowed items in the library");
    
    Console.Write("> ");
}