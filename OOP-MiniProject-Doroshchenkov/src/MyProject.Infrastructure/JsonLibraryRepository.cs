using System.Text.Json;
using MyProject.Domain;

namespace MyProject.Infrastructure;

public class JsonLibraryRepository : ILibraryRepository
{
    private readonly string _filePath;
    private List<Book> _books = new();

    public JsonLibraryRepository(string filePath)
    {
        _filePath = filePath;

        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);

            if (!string.IsNullOrWhiteSpace(json))
            {
                _books = JsonSerializer.Deserialize<List<Book>>(json)
                         ?? new List<Book>();
            }
        }
    }

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
        var json = JsonSerializer.Serialize(_books,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_filePath, json);
    }
}