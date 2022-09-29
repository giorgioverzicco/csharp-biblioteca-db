using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace csharp_biblioteca;

public class User
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }
    public string PhoneNumber { get; }
    public List<Item> BorrowedItems { get; } = new();
    
    public User(
        string firstName, 
        string lastName, 
        string email, 
        string password, 
        string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        
        if (!IsValidEmail(email)) 
            throw new ArgumentException("The email provided is not valid.");
        
        Email = email;
        Password = HashPassword(password);
        
        if (!IsValidItalianPhoneNumber(phoneNumber))
            throw new ArgumentException("The phone number provided is not a valid italian phone number.");
        
        PhoneNumber = phoneNumber;
    }

    public Item SearchByCode(Library library, string code)
    {
        return library.FindByCode(code);
    }
    
    public List<Item> SearchByTitle(Library library, string title)
    {
        return library.FindByTitle(title);
    }

    public void Borrow(Library library, Item item)
    {
        library.Borrow(item);

        library.ItemBorrowLogs.Add(
            new ItemBorrowLog(this, item, DateTime.Now));
        
        BorrowedItems.Add(item);
    }

    public void Return(Library library, Item item)
    {
        library.Return(item);
        
        library.ItemReturnLogs.Add(
            new ItemReturnLog(this, item, DateTime.Now));
        
        BorrowedItems.Remove(item);
    }

    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(
            email, 
            @"^[A-Za-z0-9_!#$%&'*+\/=?`{|}~^.-]+@[A-Za-z0-9.-]+$");
    }

    private bool IsValidItalianPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(
            phoneNumber, 
            @"^(\((00|\+)39\)|(00|\+)39)?(39[2]|38[890]|34[7-90]|36[680]|33[3-90]|32[89])\d{7}$");
    }

    private string HashPassword(string password)
    {
        SHA256 sha256 = SHA256.Create();
        
        byte[] hashedPasswordBytes = 
            sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        
        string hashedPassword = 
            BitConverter.ToString(hashedPasswordBytes)
                .Replace("-", "")
                .ToLower();
        
        return hashedPassword;
    }
}