namespace ElPerrito.Core.Reports
{
    public class PdfReportFactory : IReportFactory
    {
        public IReport CreateSalesReport() => new PdfReport("Ventas");
        public IReport CreateProductReport() => new PdfReport("Productos");
        public IReport CreateInventoryReport() => new PdfReport("Inventario");
        public IReport CreateCustomerReport() => new PdfReport("Clientes");
    }
}
