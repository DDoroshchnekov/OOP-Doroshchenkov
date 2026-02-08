using System;

namespace Lab23.Bad
{
    // DIP порушено: створює залежності всередині
    // ISP порушено: "комбайн" робить все одразу
    public class BadPayrollSystem
    {
        private readonly SalaryCalculator _calculator;
        private readonly PdfExporter _pdfExporter;
        private readonly SqlDatabase _database;

        public BadPayrollSystem()
        {
            _calculator = new SalaryCalculator();
            _pdfExporter = new PdfExporter();
            _database = new SqlDatabase();
        }

        public void ProcessPayroll(int employeeId, int hours, decimal hourlyRate)
        {
            var salary = _calculator.Calculate(hours, hourlyRate);

            _database.SaveSalary(employeeId, salary);

            var pdf = _pdfExporter.Export(employeeId, salary);
            _database.SavePdf(employeeId, pdf);

            Console.WriteLine($"[BAD] Employee #{employeeId} salary = {salary} грн");
        }
    }

    public class SalaryCalculator
    {
        public decimal Calculate(int hours, decimal hourlyRate) => hours * hourlyRate;
    }

    public class PdfExporter
    {
        public byte[] Export(int employeeId, decimal salary)
        {
            var text = $"PAYROLL REPORT\nEmployee: {employeeId}\nSalary: {salary} грн\n";
            return System.Text.Encoding.UTF8.GetBytes(text);
        }
    }

    public class SqlDatabase
    {
        public void SaveSalary(int employeeId, decimal salary)
            => Console.WriteLine($"[DB] Saved salary {salary} for employee #{employeeId}");

        public void SavePdf(int employeeId, byte[] pdf)
            => Console.WriteLine($"[DB] Saved report ({pdf.Length} bytes) for employee #{employeeId}");
    }
}
