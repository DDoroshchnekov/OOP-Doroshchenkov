using MyProject.Application;
using MyProject.Infrastructure;

var repository = new JsonLibraryRepository("books.json");

var service = new LibraryService(repository);

if (!service.GetAvailableBooks().Any())
{
    service.AddBook("Clean Code", "Robert Martin");
    service.AddBook("C# in Depth", "Jon Skeet");

    service.Save();
}

while (true)
{
    Console.WriteLine("\n1. Show books");
    Console.WriteLine("2. Borrow book");
    Console.WriteLine("3. Save");
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

        case "3":

            service.Save();

            Console.WriteLine("Saved");

            break;

        case "0":

            service.Save();

            return;
    }
}