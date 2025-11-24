using System;
using System.Linq;
using Lab5.Models;
using Lab5.Repository;
using Lab5.Exceptions;

namespace Lab5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var repo = new Repository<Item>(item => item.Id);

                repo.Add(new Item(1, "Молоко", 35.5));
                repo.Add(new Item(2, "Хліб", 25.0));
                repo.Add(new Item(3, "Цукор", 42.3));
                repo.Add(new Item(4, "Яблука", 50.0));

                Console.WriteLine("📦 Всі товари:");
                foreach (var i in repo.GetAll())
                    Console.WriteLine(i);

                // LINQ: вибираємо товари з ціною > 30
                var expensive = repo.GetAll().Where(i => i.Price > 30);
                Console.WriteLine("\n💰 Товари дорожчі за 30 грн:");
                foreach (var i in expensive)
                    Console.WriteLine(i);

                // Знаходимо елемент по ID
                Console.WriteLine("\n🔍 Пошук товару з ID = 2:");
                Console.WriteLine(repo.GetById(2));

                // Видаляємо товар
                Console.WriteLine("\n🗑 Видаляю товар з ID = 3...");
                repo.Remove(3);

                Console.WriteLine("\n✅ Залишилися товари:");
                foreach (var i in repo.GetAll())
                    Console.WriteLine(i);

                // Виклик виключення (спроба видалити неіснуючий елемент)
                Console.WriteLine("\n❌ Спроба видалити товар з ID = 99...");
                repo.Remove(99);
            }
            catch (InvalidItemException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Невідома помилка: {ex.Message}");
            }
        }
    }
}
