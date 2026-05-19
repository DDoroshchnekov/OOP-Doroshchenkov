namespace MyProject.Domain;

public class Book
{
    public Guid Id { get; }
    public string Title { get; }
    public string Author { get; }
    public bool IsAvailable { get; private set; }

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