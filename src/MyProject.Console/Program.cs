using MyProject.Application;
using MyProject.Infrastructure;

var repository = new InMemoryLibraryRepository();
var service = new LibraryService(repository);

service.AddBook("Clean Code", "Robert Martin");
service.AddBook("C# in Depth", "Jon Skeet");

while (true)
{
    Console.WriteLine("\n1. Show books");
    Console.WriteLine("2. Borrow book");
    Console.WriteLine("0. Exit");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            var books = service.GetAvailableBooks();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id} | {book.Title} | {book.Author}");
            }

            break;

        case "2":
            Console.Write("Enter book id: ");

            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                try
                {
                    service.BorrowBook(id);
                    Console.WriteLine("Book borrowed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            break;

        case "0":
            return;
    }
}