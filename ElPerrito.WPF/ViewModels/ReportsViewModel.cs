using ElPerrito.WPF.Commands;
using System;
using System.Windows.Input;
using System.Windows;

namespace ElPerrito.WPF.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        private DateTime? _salesReportStartDate;
        private DateTime? _salesReportEndDate;

        public ReportsViewModel()
        {
            // Comandos
            GenerateSalesReportCommand = new RelayCommand(format => GenerateSalesReport(format));
            GenerateInventoryReportCommand = new RelayCommand(format => GenerateInventoryReport(format));
            GenerateClientsReportCommand = new RelayCommand(format => GenerateClientsReport(format));

            // Fechas por defecto
            SalesReportStartDate = DateTime.Now.AddMonths(-1);
            SalesReportEndDate = DateTime.Now;
        }

        public DateTime? SalesReportStartDate
        {
            get => _salesReportStartDate;
            set => SetProperty(ref _salesReportStartDate, value);
        }

        public DateTime? SalesReportEndDate
        {
            get => _salesReportEndDate;
            set => SetProperty(ref _salesReportEndDate, value);
        }

        public ICommand GenerateSalesReportCommand { get; }
        public ICommand GenerateInventoryReportCommand { get; }
        public ICommand GenerateClientsReportCommand { get; }

        private void GenerateSalesReport(object? format)
        {
            string formatStr = format?.ToString() ?? "PDF";
            MessageBox.Show($"Generando reporte de ventas en formato {formatStr}...",
                          "Generar Reporte",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
            // TODO: Implementar generación real de reporte
        }

        private void GenerateInventoryReport(object? format)
        {
            string formatStr = format?.ToString() ?? "PDF";
            MessageBox.Show($"Generando reporte de inventario en formato {formatStr}...",
                          "Generar Reporte",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
            // TODO: Implementar generación real de reporte
        }

        private void GenerateClientsReport(object? format)
        {
            string formatStr = format?.ToString() ?? "PDF";
            MessageBox.Show($"Generando reporte de clientes en formato {formatStr}...",
                          "Generar Reporte",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
            // TODO: Implementar generación real de reporte
        }
    }
}
