using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ElPerrito.Core.Logging;

namespace ElPerrito.Core.Reports
{
    public class ExcelReport : IReport
    {
        private readonly Logger _logger = Logger.Instance;
        private readonly string _reportType;

        public ExcelReport(string reportType)
        {
            _reportType = reportType;
        }

        public async Task<byte[]> GenerateAsync<T>(List<T> data, string title)
        {
            await Task.Delay(150); // Simular generación de Excel

            _logger.LogInfo($"Generando reporte Excel: {title} ({_reportType})");

            // Simulación: En producción usarías EPPlus o ClosedXML
            StringBuilder excelContent = new StringBuilder();
            excelContent.AppendLine($"<?xml version=\"1.0\"?>");
            excelContent.AppendLine($"<Workbook>");
            excelContent.AppendLine($"  <Worksheet name=\"{title}\">");
            excelContent.AppendLine($"    <Table>");
            excelContent.AppendLine($"      <Row><Cell>Reporte: {_reportType}</Cell></Row>");
            excelContent.AppendLine($"      <Row><Cell>Fecha: {DateTime.Now}</Cell></Row>");
            excelContent.AppendLine($"      <Row><Cell>Registros: {data.Count}</Cell></Row>");

            foreach (var item in data)
            {
                excelContent.AppendLine($"      <Row><Cell>{item}</Cell></Row>");
            }

            excelContent.AppendLine($"    </Table>");
            excelContent.AppendLine($"  </Worksheet>");
            excelContent.AppendLine($"</Workbook>");

            return Encoding.UTF8.GetBytes(excelContent.ToString());
        }

        public string GetFileExtension() => ".xlsx";
        public string GetMimeType() => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }
}
