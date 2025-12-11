using ElPerrito.WPF.Commands;
using ElPerrito.WPF.Views;
using ElPerrito.WPF.Models;
using ElPerrito.WPF.Services;
using ElPerrito.Core.Logging;
using ElPerrito.Core.Configuration;
using System.Windows.Input;
using System.Windows.Controls;

namespace ElPerrito.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Logger _logger = Logger.Instance;
        private readonly ConfigurationManager _config = ConfigurationManager.Instance;
        private readonly AuthService _authService = new AuthService();
        private string _currentView = "Inicio";
        private string _statusMessage = "Listo";
        private UserControl? _currentViewContent;
        private readonly CartViewModel _cartViewModel;
        private readonly ShopViewModel _shopViewModel;

        public MainViewModel()
        {
            _logger.LogInfo("MainViewModel iniciado");

            // Inicializar ViewModels compartidos para tienda y carrito
            _cartViewModel = new CartViewModel();
            _shopViewModel = new ShopViewModel(_cartViewModel);

            // Conectar eventos de navegación
            _shopViewModel.NavigateToCart += ShowCart;
            _cartViewModel.NavigateToShop += ShowShop;

            // Comandos Admin
            ShowProductsCommand = new RelayCommand(_ => ShowProducts(), _ => IsAdminOrOperador);
            ShowSalesCommand = new RelayCommand(_ => ShowSales(), _ => IsAdminOrOperador);
            ShowReportsCommand = new RelayCommand(_ => ShowReports(), _ => IsAdminOrOperador);
            ShowSettingsCommand = new RelayCommand(_ => ShowSettings(), _ => IsAdminOrOperador);

            // Comandos Cliente
            ShowShopCommand = new RelayCommand(_ => ShowShop());
            ShowCartCommand = new RelayCommand(_ => ShowCart(), _ => IsCliente);

            // Comandos General
            LoginCommand = new RelayCommand(_ => ShowLogin(), _ => !IsAuthenticated);
            LogoutCommand = new RelayCommand(_ => Logout(), _ => IsAuthenticated);
            ExitCommand = new RelayCommand(_ => Exit());

            // Mensaje de bienvenida
            if (IsAuthenticated)
            {
                StatusMessage = $"Bienvenido, {CurrentSession.Instance.GetFullName()}";

                // Mostrar vista inicial según el tipo de usuario
                if (IsAdminOrOperador)
                {
                    ShowProducts();
                }
                else
                {
                    ShowShop();
                }
            }
            else
            {
                StatusMessage = "Bienvenido a El Perrito - Explora nuestros productos";
                // Modo invitado: mostrar tienda por defecto
                ShowShop();
            }
        }

        public string CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public UserControl? CurrentViewContent
        {
            get => _currentViewContent;
            set => SetProperty(ref _currentViewContent, value);
        }

        // Comandos Admin
        public ICommand ShowProductsCommand { get; }
        public ICommand ShowSalesCommand { get; }
        public ICommand ShowReportsCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        // Comandos Cliente
        public ICommand ShowShopCommand { get; }
        public ICommand ShowCartCommand { get; }

        // Comandos General
        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ExitCommand { get; }

        // Propiedades de control de sesión
        public bool IsAuthenticated => CurrentSession.Instance.IsAuthenticated;
        public bool IsCliente => CurrentSession.Instance.IsCliente;
        public bool IsAdmin => CurrentSession.Instance.IsAdmin;
        public bool IsOperador => CurrentSession.Instance.IsOperador;
        public bool IsAdminOrOperador => CurrentSession.Instance.IsAdminOrOperador;
        public string UserFullName => CurrentSession.Instance.IsAuthenticated
            ? CurrentSession.Instance.GetFullName()
            : "Invitado";
        public string UserTypeLabel => IsCliente ? "Cliente" : (IsAdmin ? "Administrador" : "Operador");

        private void ShowHome()
        {
            CurrentView = "Inicio";
            CurrentViewContent = new HomeView();
            StatusMessage = "Bienvenido a El Perrito E-commerce";
            _logger.LogInfo("Mostrando vista de inicio");
        }

        private void ShowProducts()
        {
            CurrentView = "Productos";
            var productsView = new ProductsView();
            productsView.DataContext = new ProductsViewModel();
            CurrentViewContent = productsView;
            StatusMessage = "Vista de Productos";
            _logger.LogInfo("Navegando a Productos");
        }

        private void ShowSales()
        {
            CurrentView = "Ventas";
            var salesView = new SalesView();
            salesView.DataContext = new SalesViewModel();
            CurrentViewContent = salesView;
            StatusMessage = "Vista de Ventas";
            _logger.LogInfo("Navegando a Ventas");
        }

        private void ShowReports()
        {
            CurrentView = "Reportes";
            var reportsView = new ReportsView();
            reportsView.DataContext = new ReportsViewModel();
            CurrentViewContent = reportsView;
            StatusMessage = "Vista de Reportes";
            _logger.LogInfo("Navegando a Reportes");
        }

        private void ShowSettings()
        {
            CurrentView = "Configuración";
            var settingsView = new SettingsView();
            settingsView.DataContext = new SettingsViewModel();
            CurrentViewContent = settingsView;
            StatusMessage = "Vista de Configuración";
            _logger.LogInfo("Navegando a Configuración");
        }

        private void ShowShop()
        {
            CurrentView = "Tienda";
            var shopView = new ShopView();
            shopView.DataContext = _shopViewModel;
            CurrentViewContent = shopView;
            StatusMessage = "Explora nuestro catálogo de productos";
            _logger.LogInfo("Navegando a Tienda");
        }

        private void ShowCart()
        {
            CurrentView = "Carrito de Compras";
            var cartView = new CartView();
            cartView.DataContext = _cartViewModel;
            CurrentViewContent = cartView;
            StatusMessage = $"Carrito: {_cartViewModel.TotalItems} productos";
            _logger.LogInfo("Navegando a Carrito");
        }

        private async void ShowLogin()
        {
            _logger.LogInfo("Mostrando ventana de login");

            // Abrir ventana de login como diálogo modal
            var loginWindow = new LoginView();
            loginWindow.Owner = System.Windows.Application.Current.MainWindow;
            var result = loginWindow.ShowDialog();

            // Si el login fue exitoso, refrescar la vista
            if (result == true || CurrentSession.Instance.IsAuthenticated)
            {
                // Forzar actualización de las propiedades de binding
                OnPropertyChanged(nameof(IsAuthenticated));
                OnPropertyChanged(nameof(IsCliente));
                OnPropertyChanged(nameof(IsAdmin));
                OnPropertyChanged(nameof(IsOperador));
                OnPropertyChanged(nameof(IsAdminOrOperador));
                OnPropertyChanged(nameof(UserFullName));
                OnPropertyChanged(nameof(UserTypeLabel));

                // Actualizar mensaje de estado
                StatusMessage = $"Bienvenido, {CurrentSession.Instance.GetFullName()}";

                // Cargar carrito si es cliente
                if (IsCliente)
                {
                    await _cartViewModel.LoadCartFromDatabase();
                }

                // Mostrar vista según tipo de usuario
                if (IsAdminOrOperador)
                {
                    ShowProducts();
                }
                else
                {
                    ShowShop();
                }
            }
        }

        private void Logout()
        {
            _logger.LogInfo("Cerrando sesión");

            // Limpiar carrito local antes de cerrar sesión
            _cartViewModel.ClearCartLocal();

            _authService.Logout();

            // Forzar actualización de las propiedades de binding
            OnPropertyChanged(nameof(IsAuthenticated));
            OnPropertyChanged(nameof(IsCliente));
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsOperador));
            OnPropertyChanged(nameof(IsAdminOrOperador));
            OnPropertyChanged(nameof(UserFullName));
            OnPropertyChanged(nameof(UserTypeLabel));

            // Actualizar mensaje y volver a tienda
            StatusMessage = "Sesión cerrada - Bienvenido a El Perrito";
            ShowShop();
        }

        private void Exit()
        {
            _logger.LogInfo("Cerrando aplicación");
            System.Windows.Application.Current.Shutdown();
        }
    }
}
