using System;
using System.Collections.Generic;

// Інтерфейс для обчислення площі
interface IArea
{
    double GetArea();
}

// Абстрактний клас
abstract class Shape : IArea
{
    public string Name { get; set; }

    public Shape(string name)
    {
        Name = name;
    }

    public abstract double GetArea();

    public override string ToString()
    {
        return $"{Name} (площа = {GetArea():F2})";
    }
}

// Клас Коло
class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius) : base("Коло")
    {
        Radius = radius;
    }

    public override double GetArea() => Math.PI * Radius * Radius;
}

// Клас Прямокутник
class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height) : base("Прямокутник")
    {
        Width = width;
        Height = height;
    }

    public override double GetArea() => Width * Height;
}

// Клас для обчислень (композиція)
class ShapeCalculator
{
    private List<Shape> shapes = new List<Shape>();

    public void AddShape(Shape shape)
    {
        shapes.Add(shape);
    }

    public double GetTotalArea()
    {
        double total = 0;
        foreach (var s in shapes)
            total += s.GetArea();
        return total;
    }

    public Shape GetMaxAreaShape()
    {
        Shape max = shapes[0];
        foreach (var s in shapes)
            if (s.GetArea() > max.GetArea())
                max = s;
        return max;
    }

    public Shape GetMinAreaShape()
    {
        Shape min = shapes[0];
        foreach (var s in shapes)
            if (s.GetArea() < min.GetArea())
                min = s;
        return min;
    }
}

class Program
{
    static void Main()
    {
        var calc = new ShapeCalculator();

        calc.AddShape(new Circle(5));
        calc.AddShape(new Rectangle(4, 6));
        calc.AddShape(new Circle(2));

        Console.WriteLine("Список фігур:");
        Console.WriteLine("----------------------");

        foreach (var shape in new List<Shape> {
            new Circle(5),
            new Rectangle(4, 6),
            new Circle(2)
        })
        {
            Console.WriteLine(shape);
        }

        Console.WriteLine("\n----------------------");
        Console.WriteLine($"Сумарна площа: {calc.GetTotalArea():F2}");
        Console.WriteLine($"Найменша фігура: {calc.GetMinAreaShape()}");
        Console.WriteLine($"Найбільша фігура: {calc.GetMaxAreaShape()}");
    }
}
