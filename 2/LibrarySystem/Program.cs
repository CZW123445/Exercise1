using System;
using System.Collections.Generic;

// Task 1
public enum ItemType
{
    Novels, Magazine, TextBook
}

public abstract class LibraryItem
{
    private readonly int id;
    private readonly string title;
    private readonly ItemType itemType;

    public int Id => id;
    public string Title => title;
    public ItemType ItemType => itemType;

    public LibraryItem(int id, string title, ItemType itemType)
    {
        this.id = id;
        this.title = title;
        this.itemType = itemType;
    }

    public abstract string GetDetails();
}

// Task 2
public class Novel : LibraryItem
{
    private readonly string author;

    public string Author => author;

    public Novel(int id, string title, string author) : base(id, title, ItemType.Novels)
    {
        this.author = author;
    }

    public override string GetDetails()
    {
        return $"Novel: {Title} by {Author}";
    }
}

// Task 3
public class Magazine : LibraryItem
{
    private readonly int issueNumber;

    public int IssueNumber => issueNumber;

    public Magazine(int id, string title, int issueNumber) : base(id, title, ItemType.Magazine)
    {
        this.issueNumber = issueNumber;
    }

    public override string GetDetails()
    {
        return $"Magazine: {Title} - Issue #{IssueNumber}";
    }
}

// Task 4
public class TextBook : LibraryItem
{
    private readonly string publisher;

    public string Publisher => publisher;

    public TextBook(int id, string title, string publisher) : base(id, title, ItemType.TextBook)
    {
        this.publisher = publisher;
    }

    public override string GetDetails()
    {
        return $"TextBook: {Title} by {Publisher}";
    }
}

// Task 5
public class Member
{
    private readonly string name;
    private readonly List<LibraryItem> borrowedItems;

    public string Name => name;
    public List<LibraryItem> BorrowedItems => borrowedItems;

    public Member(string name)
    {
        this.name = name;
        this.borrowedItems = new List<LibraryItem>();
    }

    public string BorrowItem(LibraryItem item)
    {
        if (borrowedItems.Count >= 3)
        {
            return "You cannot borrow more than 3 items.";
        }
        else
        {
            borrowedItems.Add(item);
            return $"Item '{item.Title}' has been added to {Name}'s list of borrowed books.";
        }
    }

    public string ReturnItem(LibraryItem item)
    {
        if (borrowedItems.Contains(item))
        {
            borrowedItems.Remove(item);
            return $"Item '{item.Title}' has been successfully returned.";
        }
        else
        {
            return $"Item '{item.Title}' was not in the list of borrowed items.";
        }
    }

    public List<LibraryItem> GetBorrowedItems()
    {
        return borrowedItems;
    }
}

// Task 6
public class LibraryManager
{
    private readonly List<LibraryItem> catalog;
    private readonly List<Member> members;

    public LibraryManager()
    {
        catalog = new List<LibraryItem>();
        members = new List<Member>();
    }

    public void AddItem(LibraryItem item)
    {
        catalog.Add(item);
    }

    public void RegisterMember(Member member)
    {
        members.Add(member);
    }

    public void ShowCatalog()
    {
        Console.WriteLine("=== Library Catalog ===");
        foreach (var item in catalog)
        {
            Console.WriteLine(item.GetDetails());
        }
        Console.WriteLine();
    }

    public LibraryItem? FindItemById(int id) => catalog.Find(i => i.Id == id);

    public Member? FindMemberByName(string name) => members.Find(m => m.Name == name);
}

// Task 7
class Program
{
    static void Main(string[] args)
    {
        LibraryManager library = new LibraryManager();

        library.AddItem(new Novel(1, "To Kill a Mockingbird", "Harper Lee"));
        library.AddItem(new Magazine(2, "National Geographic", 202));
        library.AddItem(new TextBook(3, "Introduction to Algorithms", "MIT Press"));

        Member alice = new Member("Alice");
        Member bob = new Member("Bob");

        library.RegisterMember(alice);
        library.RegisterMember(bob);

        library.ShowCatalog();

        LibraryItem? item = library.FindItemById(1);
        if (item != null)
        {
            Console.WriteLine(alice.BorrowItem(item));
        }

        item = library.FindItemById(2);
        if (item != null)
        {
            Console.WriteLine(alice.BorrowItem(item));
        }

        item = library.FindItemById(3);
        if (item != null)
        {
            Console.WriteLine(alice.BorrowItem(item));
        }

        Novel newNovel = new Novel(4, "1984", "George Orwell");
        library.AddItem(newNovel);
        Console.WriteLine(alice.BorrowItem(newNovel));

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}