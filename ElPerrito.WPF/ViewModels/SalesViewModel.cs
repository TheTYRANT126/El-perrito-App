using ElPerrito.WPF.Commands;
using ElPerrito.WPF.Models;
using ElPerrito.WPF.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ElPerrito.WPF.ViewModels
{
    public class SalesViewModel : ViewModelBase
    {
        private VentaViewModel? _selectedSale;
        private DateTime? _filterStartDate;
        private DateTime? _filterEndDate;
        private decimal _totalSalesToday;
        private decimal _totalSalesMonth;
        private int _pendingOrders;
        private int _totalOrders;
        private readonly VentaService _ventaService;

        public SalesViewModel()
        {
            _ventaService = new VentaService();
            Sales = new ObservableCollection<VentaViewModel>();

            // Comandos
            ViewSaleCommand = new RelayCommand(sale => ViewSale(sale));
            ApplyFilterCommand = new RelayCommand(_ => ApplyFilter());

            // Cargar datos desde la base de datos
            LoadSalesFromDatabase();
        }

        public ObservableCollection<VentaViewModel> Sales { get; }

        public VentaViewModel? SelectedSale
        {
            get => _selectedSale;
            set => SetProperty(ref _selectedSale, value);
        }

        public DateTime? FilterStartDate
        {
            get => _filterStartDate;
            set => SetProperty(ref _filterStartDate, value);
        }

        public DateTime? FilterEndDate
        {
            get => _filterEndDate;
            set => SetProperty(ref _filterEndDate, value);
        }

        public decimal TotalSalesToday
        {
            get => _totalSalesToday;
            set => SetProperty(ref _totalSalesToday, value);
        }

        public decimal TotalSalesMonth
        {
            get => _totalSalesMonth;
            set => SetProperty(ref _totalSalesMonth, value);
        }

        public int PendingOrders
        {
            get => _pendingOrders;
            set => SetProperty(ref _pendingOrders, value);
        }

        public int TotalOrders
        {
            get => _totalOrders;
            set => SetProperty(ref _totalOrders, value);
        }

        public ICommand ViewSaleCommand { get; }
        public ICommand ApplyFilterCommand { get; }

        private void ViewSale(object? sale)
        {
            // TODO: Abrir ventana de detalles de venta
        }

        private void ApplyFilter()
        {
            // TODO: Aplicar filtro de fechas
        }

        private async void LoadSalesFromDatabase()
        {
            try
            {
                // Cargar ventas
                var ventas = await _ventaService.ObtenerVentasAsync();
                Sales.Clear();
                foreach (var venta in ventas)
                {
                    Sales.Add(venta);
                }

                // Cargar estadísticas
                var (totalHoy, totalMes, ordenesCompletas, ordenesPendientes) = await _ventaService.ObtenerEstadisticasAsync();
                TotalSalesToday = totalHoy;
                TotalSalesMonth = totalMes;
                TotalOrders = ordenesCompletas;
                PendingOrders = ordenesPendientes;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }
    }
}
