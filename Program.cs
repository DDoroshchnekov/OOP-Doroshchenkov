using System;

// Інтерфейс
public interface IReport
{
    void Generate(string data);
}

// Реалізації звітів
public class TextReport : IReport
{
    public void Generate(string data)
    {
        Console.WriteLine($"[TEXT REPORT] {data}");
    }
}

public class HtmlReport : IReport
{
    public void Generate(string data)
    {
        Console.WriteLine($"[HTML REPORT] <html><body>{data}</body></html>");
    }
}

public class JsonReport : IReport
{
    public void Generate(string data)
    {
        Console.WriteLine($"[JSON REPORT] {{ \"report\": \"{data}\" }}");
    }
}

// Абстрактна фабрика
public abstract class ReportFactory
{
    protected abstract IReport CreateReport();

    public void GenerateReport(string data)
    {
        IReport report = CreateReport();
        report.Generate(data);
    }
}

// Конкретні фабрики
public class TextReportFactory : ReportFactory
{
    protected override IReport CreateReport()
    {
        return new TextReport();
    }
}

public class HtmlReportFactory : ReportFactory
{
    protected override IReport CreateReport()
    {
        return new HtmlReport();
    }
}

public class JsonReportFactory : ReportFactory
{
    protected override IReport CreateReport()
    {
        return new JsonReport();
    }
}

// Singleton
public class ReportService
{
    private static ReportService _instance;

    private ReportFactory _factory;

    private ReportService() { }

    public static ReportService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ReportService();
            }

            return _instance;
        }
    }

    public void SetReportFactory(ReportFactory factory)
    {
        _factory = factory;
    }

    public void CreateReport(string data)
    {
        if (_factory == null)
        {
            Console.WriteLine("Factory is not set!");
            return;
        }

        _factory.GenerateReport(data);
    }
}

// Головна програма
class Program
{
    static void Main(string[] args)
    {
        ReportService service = ReportService.Instance;

        // Text Report
        service.SetReportFactory(new TextReportFactory());

        service.CreateReport("Sales report for May");
        service.CreateReport("Inventory report");

        Console.WriteLine();

        // HTML Report
        service.SetReportFactory(new HtmlReportFactory());

        service.CreateReport("Employee report");
        service.CreateReport("Finance report");

        Console.WriteLine();

        // JSON Report
        service.SetReportFactory(new JsonReportFactory());

        service.CreateReport("Analytics report");
        service.CreateReport("Security report");
    }
}