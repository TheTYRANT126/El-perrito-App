namespace ElPerrito.Core.Reports
{
    /// <summary>
    /// Abstract Factory para crear familias de reportes
    /// </summary>
    public interface IReportFactory
    {
        IReport CreateSalesReport();
        IReport CreateProductReport();
        IReport CreateInventoryReport();
        IReport CreateCustomerReport();
    }
}
