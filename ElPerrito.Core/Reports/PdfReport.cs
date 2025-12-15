using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ElPerrito.Core.Logging;

namespace ElPerrito.Core.Reports
{
    public class PdfReport : IReport
    {
        private readonly Logger _logger = Logger.Instance;
        private readonly string _reportType;

        public PdfReport(string reportType)
        {
            _reportType = reportType;
        }

        public async Task<byte[]> GenerateAsync<T>(List<T> data, string title)
        {
            await Task.Delay(200); // Simular generación de PDF

            _logger.LogInfo($"Generando reporte PDF: {title} ({_reportType})");

            // Simulación: En producción usarías una librería como iTextSharp o QuestPDF
            StringBuilder pdfContent = new StringBuilder();
            pdfContent.AppendLine($"%PDF-1.4");
            pdfContent.AppendLine($"% Reporte: {title}");
            pdfContent.AppendLine($"% Tipo: {_reportType}");
            pdfContent.AppendLine($"% Fecha: {DateTime.Now}");
            pdfContent.AppendLine($"% Registros: {data.Count}");

            foreach (var item in data)
            {
                pdfContent.AppendLine(item?.ToString() ?? "null");
            }

            return Encoding.UTF8.GetBytes(pdfContent.ToString());
        }

        public string GetFileExtension() => ".pdf";
        public string GetMimeType() => "application/pdf";
    }
}
