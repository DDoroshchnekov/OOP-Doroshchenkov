using System;
using Lab23.Bad;
using Lab23.Good;
class Program
{
    static void Main()
    {
        Console.WriteLine("===== BAD (violates ISP + DIP) =====");
        var bad = new BadPayrollSystem();
        bad.ProcessPayroll(employeeId: 7, hours: 160, hourlyRate: 120);
        Console.WriteLine();
        Console.WriteLine("===== GOOD (ISP + DIP + Constructor DI) =====");
        ISalaryCalculator calculator = new DefaultSalaryCalculator();
        IReportExporter exporter = new TextReportExporter();
        IPayrollRepository repository = new ConsolePayrollRepository();
        var good = new PayrollSystem(calculator, exporter, repository);
        good.ProcessPayroll(employeeId: 7, hours: 160, hourlyRate: 120);
        Console.WriteLine("\nDone.");
    }
}
