using MyProject.Domain;

namespace MyProject.Application;

public class LibraryService
{
    private readonly ILibraryRepository _repository;

    public LibraryService(ILibraryRepository repository)
    {
        _repository = repository;
    }

    public void AddBook(string title, string author)
    {
        var book = new Book(title, author);

        _repository.AddBook(book);
    }

    public void BorrowBook(Guid id)
    {
        var book = _repository.GetBookById(id);

        if (book == null)
            throw new Exception("Book not found");

        book.Borrow();
    }

    public List<Book> GetAvailableBooks()
    {
        return _repository
            .GetBooks()
            .Where(b => b.IsAvailable)
            .ToList();
    }

    public void Save()
    {
        _repository.Save();
    }
}