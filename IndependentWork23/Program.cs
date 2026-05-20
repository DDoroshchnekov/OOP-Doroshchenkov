using System;

// =======================
// ADAPTER
// =======================

// Target
public interface IRenderer
{
    void RenderShape();
}

// Adaptee
public class DirectXRenderer
{
    public void DrawPrimitive(int type, float[] vertices)
    {
        Console.WriteLine($"DirectX: Drawing primitive type {type}");
    }
}

// Adapter
public class DirectXAdapter : IRenderer
{
    private DirectXRenderer _renderer;

    public DirectXAdapter(DirectXRenderer renderer)
    {
        _renderer = renderer;
    }

    public void RenderShape()
    {
        _renderer.DrawPrimitive(1, new float[] { 0, 1, 2 });
    }
}

// =======================
// FACADE
// =======================

public class ShapeFactory
{
    public void CreateCircle()
    {
        Console.WriteLine("Circle created");
    }

    public void CreateRectangle()
    {
        Console.WriteLine("Rectangle created");
    }
}

public class ColorManager
{
    public void SetColor(string color)
    {
        Console.WriteLine($"Color set to {color}");
    }
}

public class DrawingFacade
{
    private ShapeFactory _shapeFactory;
    private ColorManager _colorManager;

    public DrawingFacade()
    {
        _shapeFactory = new ShapeFactory();
        _colorManager = new ColorManager();
    }

    public void DrawColoredShape(string shape, string color)
    {
        _colorManager.SetColor(color);

        if (shape == "circle")
        {
            _shapeFactory.CreateCircle();
        }
        else if (shape == "rectangle")
        {
            _shapeFactory.CreateRectangle();
        }

        Console.WriteLine("Shape rendered successfully");
    }
}

// =======================
// PROXY
// =======================

public interface IGraphicObject
{
    void Render();
}

// Real Subject
public class RealGraphicObject : IGraphicObject
{
    public RealGraphicObject()
    {
        Console.WriteLine("Loading heavy graphic object...");
    }

    public void Render()
    {
        Console.WriteLine("Rendering graphic object");
    }
}

// Proxy
public class LazyGraphicObjectProxy : IGraphicObject
{
    private RealGraphicObject _realObject;

    public void Render()
    {
        if (_realObject == null)
        {
            _realObject = new RealGraphicObject();
        }

        Console.WriteLine("Proxy: Access granted");
        _realObject.Render();
    }
}

// =======================
// MAIN
// =======================

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ADAPTER ===");

        IRenderer renderer = new DirectXAdapter(new DirectXRenderer());
        renderer.RenderShape();

        Console.WriteLine();

        Console.WriteLine("=== FACADE ===");

        DrawingFacade facade = new DrawingFacade();

        facade.DrawColoredShape("circle", "Red");
        Console.WriteLine();

        facade.DrawColoredShape("rectangle", "Blue");

        Console.WriteLine();

        Console.WriteLine("=== PROXY ===");

        IGraphicObject graphic = new LazyGraphicObjectProxy();

        Console.WriteLine("First render:");
        graphic.Render();

        Console.WriteLine();

        Console.WriteLine("Second render:");
        graphic.Render();
    }
}