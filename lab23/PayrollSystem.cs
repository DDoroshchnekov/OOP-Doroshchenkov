using System;

namespace Lab23.Good
{
    // ISP: вузькі інтерфейси
    public interface ISalaryCalculator
    {
        decimal Calculate(int hours, decimal hourlyRate);
    }

    public interface IReportExporter
    {
        byte[] Export(int employeeId, decimal salary);
    }

    public interface IPayrollRepository
    {
        void SaveSalary(int employeeId, decimal salary);
        void SaveReport(int employeeId, byte[] reportBytes);
    }

    // DIP: PayrollSystem залежить від абстракцій, а не від конкретних класів
    // DI: залежності передаємо через конструктор
    public class PayrollSystem
    {
        private readonly ISalaryCalculator _calculator;
        private readonly IReportExporter _exporter;
        private readonly IPayrollRepository _repository;

        public PayrollSystem(ISalaryCalculator calculator, IReportExporter exporter, IPayrollRepository repository)
        {
            _calculator = calculator;
            _exporter = exporter;
            _repository = repository;
        }

        public void ProcessPayroll(int employeeId, int hours, decimal hourlyRate)
        {
            var salary = _calculator.Calculate(hours, hourlyRate);

            _repository.SaveSalary(employeeId, salary);

            var report = _exporter.Export(employeeId, salary);
            _repository.SaveReport(employeeId, report);

            Console.WriteLine($"[GOOD] Employee #{employeeId} salary = {salary} грн");
        }
    }

    // Реалізації (можна легко замінити на інші)
    public class DefaultSalaryCalculator : ISalaryCalculator
    {
        public decimal Calculate(int hours, decimal hourlyRate) => hours * hourlyRate;
    }

    public class TextReportExporter : IReportExporter
    {
        public byte[] Export(int employeeId, decimal salary)
        {
            var text = $"PAYROLL REPORT\nEmployee: {employeeId}\nSalary: {salary} грн\n";
            return System.Text.Encoding.UTF8.GetBytes(text);
        }
    }

    public class ConsolePayrollRepository : IPayrollRepository
    {
        public void SaveSalary(int employeeId, decimal salary)
            => Console.WriteLine($"[Repo] Salary saved: {salary} for employee #{employeeId}");

        public void SaveReport(int employeeId, byte[] reportBytes)
            => Console.WriteLine($"[Repo] Report saved: {reportBytes.Length} bytes for employee #{employeeId}");
    }
}
