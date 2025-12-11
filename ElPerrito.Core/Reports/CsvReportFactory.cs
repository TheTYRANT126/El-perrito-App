namespace ElPerrito.Core.Reports
{
    /// <summary>
    /// Concrete Factory para crear reportes en CSV
    /// </summary>
    public class CsvReportFactory : IReportFactory
    {
        public IReport CreateSalesReport() => new CsvReport("Ventas");
        public IReport CreateProductReport() => new CsvReport("Productos");
        public IReport CreateInventoryReport() => new CsvReport("Inventario");
        public IReport CreateCustomerReport() => new CsvReport("Clientes");
    }
}
