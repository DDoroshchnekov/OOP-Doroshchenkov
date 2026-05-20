using System;
using System.Collections.Generic;
using System.Diagnostics;

// ==========================
// COMPONENT
// ==========================

public interface IComponent
{
    string Operation();
}

// ==========================
// LEAF
// ==========================

public class TextLeaf : IComponent
{
    private string _text;

    public TextLeaf(string text)
    {
        _text = text;
    }

    public string Operation()
    {
        return _text;
    }
}

// ==========================
// COMPOSITE
// ==========================

public class Composite : IComponent
{
    private List<IComponent> _children = new List<IComponent>();

    public void Add(IComponent component)
    {
        _children.Add(component);
    }

    public string Operation()
    {
        string result = "";

        foreach (var child in _children)
        {
            result += child.Operation() + " ";
        }

        return result.Trim();
    }
}

// ==========================
// DECORATOR
// ==========================

public abstract class Decorator : IComponent
{
    protected IComponent _component;

    public Decorator(IComponent component)
    {
        _component = component;
    }

    public virtual string Operation()
    {
        return _component.Operation();
    }
}

public class UpperCaseDecorator : Decorator
{
    public UpperCaseDecorator(IComponent component)
        : base(component)
    {
    }

    public override string Operation()
    {
        return _component.Operation().ToUpper();
    }
}

public class BracketDecorator : Decorator
{
    public BracketDecorator(IComponent component)
        : base(component)
    {
    }

    public override string Operation()
    {
        return "[" + _component.Operation() + "]";
    }
}

// ==========================
// PROXY
// ==========================

public class ProxyComponent : IComponent
{
    private IComponent _realComponent;
    private string _cachedResult;

    public ProxyComponent(IComponent component)
    {
        _realComponent = component;
    }

    public string Operation()
    {
        if (_cachedResult == null)
        {
            Console.WriteLine("Generating result...");
            _cachedResult = _realComponent.Operation();
        }
        else
        {
            Console.WriteLine("Using cached result...");
        }

        return _cachedResult;
    }
}

// ==========================
// TESTS
// ==========================

public static class Tests
{
    public static void Run()
    {
        Console.WriteLine("\n=== TESTS ===");

        // Test 1
        IComponent leaf = new TextLeaf("hello");

        if (leaf.Operation() == "hello")
            Console.WriteLine("Test 1 Passed");
        else
            Console.WriteLine("Test 1 Failed");

        // Test 2
        IComponent upper = new UpperCaseDecorator(leaf);

        if (upper.Operation() == "HELLO")
            Console.WriteLine("Test 2 Passed");
        else
            Console.WriteLine("Test 2 Failed");

        // Test 3
        Composite composite = new Composite();

        composite.Add(new TextLeaf("one"));
        composite.Add(new TextLeaf("two"));

        if (composite.Operation() == "one two")
            Console.WriteLine("Test 3 Passed");
        else
            Console.WriteLine("Test 3 Failed");

        // Test 4 (negative)
        Composite emptyComposite = new Composite();

        if (emptyComposite.Operation() == "")
            Console.WriteLine("Test 4 Passed");
        else
            Console.WriteLine("Test 4 Failed");
    }
}

// ==========================
// MAIN
// ==========================

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== COMPOSITE ===");

        Composite document = new Composite();

        document.Add(new TextLeaf("Hello"));
        document.Add(new TextLeaf("World"));

        Console.WriteLine(document.Operation());

        Console.WriteLine();

        Console.WriteLine("=== DECORATOR ===");

        IComponent decorated =
            new BracketDecorator(
                new UpperCaseDecorator(
                    document
                )
            );

        Console.WriteLine(decorated.Operation());

        Console.WriteLine();

        Console.WriteLine("=== PROXY ===");

        IComponent proxy = new ProxyComponent(decorated);

        Stopwatch sw = new Stopwatch();

        sw.Start();
        Console.WriteLine(proxy.Operation());
        sw.Stop();

        Console.WriteLine($"First call: {sw.ElapsedTicks} ticks");

        sw.Restart();
        Console.WriteLine(proxy.Operation());
        sw.Stop();

        Console.WriteLine($"Second call: {sw.ElapsedTicks} ticks");

        // Tests
        Tests.Run();

        Console.WriteLine("\n=== REPORT ===");

        Console.WriteLine("Patterns used:");
        Console.WriteLine("- Composite");
        Console.WriteLine("- Decorator");
        Console.WriteLine("- Proxy");

        Console.WriteLine("\nPerformance conclusions:");
        Console.WriteLine("1. Proxy caching speeds up repeated calls.");
        Console.WriteLine("2. Decorators add flexibility but increase object count.");
        Console.WriteLine("3. Composite simplifies work with hierarchical objects.");
    }
}