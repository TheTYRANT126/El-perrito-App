namespace ElPerrito.Core.Reports
{
    public interface IReportFactory
    {
        IReport CreateSalesReport();
        IReport CreateProductReport();
        IReport CreateInventoryReport();
        IReport CreateCustomerReport();
    }
}
