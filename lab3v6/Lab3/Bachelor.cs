using System;
namespace Lab3
{
    public class Bachelor : Student
    {
        public Bachelor(string name, double averageMark) : base(name, averageMark) { }
        public override void Study()
        {
            Console.WriteLine($"{Name} (бакалавр) вивчає основні дисципліни.");
        }
        public override void ShowInfo()
        {
            Console.WriteLine($"[Бакалавр] {Name} — середній бал: {AverageMark}");
        }
    }
}
