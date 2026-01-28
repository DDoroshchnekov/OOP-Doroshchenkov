using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== LSP VIOLATION EXAMPLE ===");

        Rectangle rect = new Square();
        rect.Width = 5;
        rect.Height = 10;

        Console.WriteLine("Expected area = 50");
        Console.WriteLine($"Actual area = {rect.GetArea()}");

        Console.WriteLine("\n=== LSP FIXED VERSION ===");

        IShape fixedRect = new FixedRectangle(5, 10);
        IShape fixedSquare = new FixedSquare(5);

        PrintArea(fixedRect);
        PrintArea(fixedSquare);
    }

    static void PrintArea(IShape shape)
    {
        Console.WriteLine($"Area = {shape.GetArea()}");
    }
}

// ================== BAD VERSION ==================

class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }

    public int GetArea() => Width * Height;
}

class Square : Rectangle
{
    public override int Width
    {
        set { base.Width = base.Height = value; }
    }

    public override int Height
    {
        set { base.Width = base.Height = value; }
    }
}

// ================== FIXED VERSION (LSP SAFE) ==================

interface IShape
{
    int GetArea();
}

class FixedRectangle : IShape
{
    private int width;
    private int height;

    public FixedRectangle(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public int GetArea() => width * height;
}

class FixedSquare : IShape
{
    private int side;

    public FixedSquare(int side)
    {
        this.side = side;
    }

    public int GetArea() => side * side;
}
