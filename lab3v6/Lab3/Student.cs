using System;
namespace Lab3
{
    public abstract class Student
    {
        public string Name { get; set; }
        public double AverageMark { get; set; }
        public Student(string name, double averageMark)
        {
            Name = name;
            AverageMark = averageMark;
        }
        public abstract void Study();

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Ім'я: {Name}, Середній бал: {AverageMark}");
        }
    }
}
