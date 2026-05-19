using System.Text.Json.Serialization;

namespace MyProject.Domain;

public class Book
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Author { get; private set; }

    public bool IsAvailable { get; private set; }

    [JsonConstructor]
    public Book(Guid id, string title, string author, bool isAvailable)
    {
        Id = id;
        Title = title;
        Author = author;
        IsAvailable = isAvailable;
    }

    public Book(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");

        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty");

        Id = Guid.NewGuid();
        Title = title;
        Author = author;
        IsAvailable = true;
    }

    public void Borrow()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("Book already borrowed");

        IsAvailable = false;
    }

    public void Return()
    {
        IsAvailable = true;
    }
}