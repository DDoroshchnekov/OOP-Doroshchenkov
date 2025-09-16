using System;

namespace OOP_Doroshchenkov
{
    class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library();

            // Додавання книг (через оператор +)
            lib += "Пригоди Тома Сойєра";
            lib += "Кобзар";
            lib += "1984";

            Console.WriteLine("Після додавання книг:");
            lib.PrintBooks();
            Console.WriteLine($"Всього книг: {lib.BookCount}\n");

            // Доступ через індексатор
            Console.WriteLine($"Книга під номером 2: {lib[1]}\n");

            // Заміна книги (через індексатор)
            lib[1] = "Лісова пісня";
            Console.WriteLine("Після заміни другої книги:");
            lib.PrintBooks();
            Console.WriteLine($"Всього книг: {lib.BookCount}\n");

            // Видалення книги (через оператор -)
            lib -= "1984";
            Console.WriteLine("Після видалення книги \"1984\":");
            lib.PrintBooks();
            Console.WriteLine($"Всього книг: {lib.BookCount}\n");

            // Демонстрація обробки помилок
            try
            {
                var book = lib[10]; // має кинути IndexOutOfRangeException
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Очікувана помилка: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть Enter, щоб вийти...");
            Console.ReadLine();
        }
    }
}
