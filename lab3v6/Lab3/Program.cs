using System;
using System.Collections.Generic;
using System.Linq;
namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Bachelor("Іван", 85),
                new Bachelor("Марія", 91),
                new Master("Олег", 95),
                new Master("Анна", 88)
            };
            Console.WriteLine("=== Інформація про студентів ===");
            foreach (var student in students)
            {
                student.ShowInfo();
                student.Study();
                Console.WriteLine();
            }
            double avg = students.Average(s => s.AverageMark);
            Console.WriteLine($"Середній бал групи: {avg:F2}");
            double percentHigh = (students.Count(s => s.AverageMark > 90) / (double)students.Count) * 100;
            Console.WriteLine($"Відсоток студентів з балом > 90: {percentHigh:F2}%");
        }
    }
}
