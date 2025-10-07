using System;
namespace Lab3
{
    public class Master : Student
    {
        public Master(string name, double averageMark) : base(name, averageMark) { }
        public override void Study()
        {
            Console.WriteLine($"{Name} (магістр) проводить наукові дослідження.");
        }
        public override void ShowInfo()
        {
            Console.WriteLine($"[Магістр] {Name} — середній бал: {AverageMark}");
        }
    }
}
