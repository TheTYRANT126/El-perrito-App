namespace ElPerrito.Core.Reports
{
    /// <summary>
    /// Concrete Factory para crear reportes en PDF
    /// </summary>
    public class PdfReportFactory : IReportFactory
    {
        public IReport CreateSalesReport() => new PdfReport("Ventas");
        public IReport CreateProductReport() => new PdfReport("Productos");
        public IReport CreateInventoryReport() => new PdfReport("Inventario");
        public IReport CreateCustomerReport() => new PdfReport("Clientes");
    }
}
