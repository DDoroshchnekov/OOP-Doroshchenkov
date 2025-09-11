using System;
namespace lab1
{
    class Figure
    {
        private string name;
        public double Area { get; set; }
        public Figure(string name)
        {
            this.name = name;
            Console.WriteLine($"Створено фігуру: {name}");
        }
        ~Figure()
        {
            Console.WriteLine($"Фігура {name} знищена");
        }
        public string GetFigure()
        {
            return $"Фігура: {name}, Площа: {Area}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Figure circle = new Figure("Коло");
            circle.Area = 12.56;
            Console.WriteLine(circle.GetFigure());

            Figure square = new Figure("Квадрат");
            square.Area = 25.0;
            Console.WriteLine(square.GetFigure());

            Figure triangle = new Figure("Трикутник");
            triangle.Area = 10.5;
            Console.WriteLine(triangle.GetFigure());
        }
    }
}
