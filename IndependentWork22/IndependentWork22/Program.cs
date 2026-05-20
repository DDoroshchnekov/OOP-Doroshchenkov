using System;
using System.Collections.Generic;

// Component
public interface IComponent
{
    string Render();
}

// Leaf - Text
public class TextElement : IComponent
{
    public string Content { get; set; }

    public TextElement(string content)
    {
        Content = content;
    }

    public string Render()
    {
        return Content;
    }
}

// Leaf - Image
public class ImageElement : IComponent
{
    public string Src { get; set; }

    public ImageElement(string src)
    {
        Src = src;
    }

    public string Render()
    {
        return $"<img src='{Src}' />";
    }
}

// Composite
public class DivElement : IComponent
{
    private List<IComponent> _elements = new List<IComponent>();

    public void Add(IComponent component)
    {
        _elements.Add(component);
    }

    public void Remove(IComponent component)
    {
        _elements.Remove(component);
    }

    public string Render()
    {
        string result = "<div>\n";

        foreach (var element in _elements)
        {
            result += element.Render() + "\n";
        }

        result += "</div>";

        return result;
    }
}

// Decorator
public abstract class Decorator : IComponent
{
    protected IComponent _component;

    public Decorator(IComponent component)
    {
        _component = component;
    }

    public abstract string Render();
}

// Bold Decorator
public class BoldDecorator : Decorator
{
    public BoldDecorator(IComponent component)
        : base(component)
    {
    }

    public override string Render()
    {
        return $"<b>{_component.Render()}</b>";
    }
}

// Italic Decorator
public class ItalicDecorator : Decorator
{
    public ItalicDecorator(IComponent component)
        : base(component)
    {
    }

    public override string Render()
    {
        return $"<i>{_component.Render()}</i>";
    }
}

// Main
class Program
{
    static void Main(string[] args)
    {
        // Leaf objects
        IComponent text1 = new TextElement("Hello World");
        IComponent text2 = new TextElement("Composite + Decorator");
        IComponent image1 = new ImageElement("photo.png");

        // Decorated elements
        IComponent boldText = new BoldDecorator(text1);
        IComponent italicText = new ItalicDecorator(text2);

        // Composite
        DivElement mainDiv = new DivElement();

        mainDiv.Add(boldText);
        mainDiv.Add(italicText);
        mainDiv.Add(image1);

        // Nested composite
        DivElement nestedDiv = new DivElement();

        nestedDiv.Add(new BoldDecorator(
            new ItalicDecorator(
                new TextElement("Nested content")
            )
        ));

        mainDiv.Add(nestedDiv);

        // Output
        Console.WriteLine("=== HTML OUTPUT ===");
        Console.WriteLine(mainDiv.Render());
    }
}