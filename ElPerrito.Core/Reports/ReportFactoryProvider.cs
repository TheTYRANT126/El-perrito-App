using System;

namespace ElPerrito.Core.Reports
{
    public enum ReportFormat
    {
        Pdf,
        Excel,
        Csv
    }

    public static class ReportFactoryProvider
    {
        public static IReportFactory GetFactory(ReportFormat format)
        {
            return format switch
            {
                ReportFormat.Pdf => new PdfReportFactory(),
                ReportFormat.Excel => new ExcelReportFactory(),
                ReportFormat.Csv => new CsvReportFactory(),
                _ => throw new ArgumentException($"Formato de reporte no soportado: {format}")
            };
        }

        public static IReportFactory GetFactory(string format)
        {
            return format.ToLower() switch
            {
                "pdf" => new PdfReportFactory(),
                "excel" or "xlsx" => new ExcelReportFactory(),
                "csv" => new CsvReportFactory(),
                _ => throw new ArgumentException($"Formato de reporte no reconocido: {format}")
            };
        }
    }
}
