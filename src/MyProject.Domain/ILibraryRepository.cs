namespace MyProject.Domain;

public interface ILibraryRepository
{
    void AddBook(Book book);

    List<Book> GetBooks();

    Book? GetBookById(Guid id);

    void Save();
}