using System;
using System.Collections.Generic;

namespace OOP_Doroshchenkov
{
    public class Library
    {
        private List<string> books;

        public Library()
        {
            books = new List<string>();
        }

        // Властивість (тільки читання)
        public int BookCount => books.Count;

        // Індексатор
        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= books.Count)
                    throw new IndexOutOfRangeException("Індекс поза діапазоном");
                return books[index];
            }
            set
            {
                if (index < 0 || index >= books.Count)
                    throw new IndexOutOfRangeException("Індекс поза діапазоном");
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва книги не може бути порожньою");
                books[index] = value;
            }
        }

        // Оператор + — додає книгу (за назвою)
        public static Library operator +(Library lib, string book)
        {
            if (lib == null) throw new ArgumentNullException(nameof(lib));
            if (string.IsNullOrWhiteSpace(book))
                throw new ArgumentException("Назва книги не може бути порожньою");
            lib.books.Add(book);
            return lib;
        }

        // Оператор - — видаляє книгу за назвою
        public static Library operator -(Library lib, string book)
        {
            if (lib == null) throw new ArgumentNullException(nameof(lib));
            bool removed = lib.books.Remove(book);
            if (!removed)
                Console.WriteLine($"Книга \"{book}\" не знайдена у бібліотеці.");
            return lib;
        }

        // Додаткові методи (виправлені)
        public void Add(string book)
        {
            books.Add(book);
        }

        public void Remove(string book)
        {
            books.Remove(book);
        }

        public void PrintBooks()
        {
            Console.WriteLine("Книги у бібліотеці:");
            if (books.Count == 0)
            {
                Console.WriteLine("  (немає книг)");
                return;
            }
            for (int i = 0; i < books.Count; i++)
                Console.WriteLine($"  {i + 1}. {books[i]}");
        }
    }
}
