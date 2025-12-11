using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ElPerrito.Core.Logging;

namespace ElPerrito.Core.Reports
{
    /// <summary>
    /// Implementación de reporte en CSV
    /// </summary>
    public class CsvReport : IReport
    {
        private readonly Logger _logger = Logger.Instance;
        private readonly string _reportType;

        public CsvReport(string reportType)
        {
            _reportType = reportType;
        }

        public async Task<byte[]> GenerateAsync<T>(List<T> data, string title)
        {
            await Task.Delay(50); // Simular generación de CSV

            _logger.LogInfo($"Generando reporte CSV: {title} ({_reportType})");

            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine($"# Reporte: {title}");
            csvContent.AppendLine($"# Tipo: {_reportType}");
            csvContent.AppendLine($"# Fecha: {DateTime.Now}");
            csvContent.AppendLine($"# Registros: {data.Count}");
            csvContent.AppendLine();

            if (data.Count > 0)
            {
                // Obtener propiedades del tipo T
                PropertyInfo[] properties = typeof(T).GetProperties();

                // Header
                csvContent.AppendLine(string.Join(",", Array.ConvertAll(properties, p => p.Name)));

                // Datos
                foreach (var item in data)
                {
                    if (item != null)
                    {
                        List<string> values = new List<string>();
                        foreach (var prop in properties)
                        {
                            var value = prop.GetValue(item);
                            values.Add(value?.ToString()?.Replace(",", ";") ?? "");
                        }
                        csvContent.AppendLine(string.Join(",", values));
                    }
                }
            }

            return Encoding.UTF8.GetBytes(csvContent.ToString());
        }

        public string GetFileExtension() => ".csv";
        public string GetMimeType() => "text/csv";
    }
}
