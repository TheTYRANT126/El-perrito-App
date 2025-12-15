namespace ElPerrito.Core.Reports
{
    public class CsvReportFactory : IReportFactory
    {
        public IReport CreateSalesReport() => new CsvReport("Ventas");
        public IReport CreateProductReport() => new CsvReport("Productos");
        public IReport CreateInventoryReport() => new CsvReport("Inventario");
        public IReport CreateCustomerReport() => new CsvReport("Clientes");
    }
}
