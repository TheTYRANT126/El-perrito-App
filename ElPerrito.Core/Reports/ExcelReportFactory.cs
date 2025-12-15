namespace ElPerrito.Core.Reports
{
    public class ExcelReportFactory : IReportFactory
    {
        public IReport CreateSalesReport() => new ExcelReport("Ventas");
        public IReport CreateProductReport() => new ExcelReport("Productos");
        public IReport CreateInventoryReport() => new ExcelReport("Inventario");
        public IReport CreateCustomerReport() => new ExcelReport("Clientes");
    }
}
