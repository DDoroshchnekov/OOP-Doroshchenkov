using MyProject.Domain;

namespace MyProject.Infrastructure;

public class InMemoryLibraryRepository : ILibraryRepository
{
    private readonly List<Book> _books = new();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public List<Book> GetBooks()
    {
        return _books;
    }

    public Book? GetBookById(Guid id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public void Save()
    {
    }
}