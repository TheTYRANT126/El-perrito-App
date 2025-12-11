using ElPerrito.WPF.Commands;
using ElPerrito.WPF.Models;
using ElPerrito.WPF.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ElPerrito.WPF.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {
        private string _searchText = string.Empty;
        private string _selectedCategory = "Todas";
        private int _cartItemCount;
        private readonly CartViewModel _cartViewModel;
        private readonly ProductoService _productoService;

        public ShopViewModel(CartViewModel cartViewModel)
        {
            _cartViewModel = cartViewModel;
            _productoService = new ProductoService();
            Products = new ObservableCollection<ProductoTiendaViewModel>();
            AllProducts = new ObservableCollection<ProductoTiendaViewModel>();
            Categories = new ObservableCollection<string> { "Todas", "Alimentos", "Medicinas", "Accesorios", "Higiene", "Juguetes" };

            // Comandos
            SearchCommand = new RelayCommand(_ => Search());
            AddToCartCommand = new RelayCommand(product => AddToCart(product));
            ViewCartCommand = new RelayCommand(_ => ViewCart());

            // Cargar productos desde la base de datos
            LoadProductsFromDatabase();

            // Sincronizar contador del carrito
            UpdateCartCount();
        }

        public ObservableCollection<ProductoTiendaViewModel> Products { get; }
        public ObservableCollection<ProductoTiendaViewModel> AllProducts { get; }
        public ObservableCollection<string> Categories { get; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                Search();
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                Search();
            }
        }

        public int CartItemCount
        {
            get => _cartItemCount;
            set => SetProperty(ref _cartItemCount, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand AddToCartCommand { get; }
        public ICommand ViewCartCommand { get; }

        public event System.Action? NavigateToCart;

        private void Search()
        {
            Products.Clear();

            var filtered = AllProducts.AsEnumerable();

            // Filtrar por categoría
            if (SelectedCategory != "Todas")
            {
                filtered = filtered.Where(p => p.Categoria == SelectedCategory);
            }

            // Filtrar por búsqueda
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(p =>
                    p.Nombre.ToLower().Contains(SearchText.ToLower()) ||
                    p.Descripcion.ToLower().Contains(SearchText.ToLower()));
            }

            // Solo productos activos
            filtered = filtered.Where(p => p.Activo);

            foreach (var product in filtered)
            {
                Products.Add(product);
            }
        }

        private void AddToCart(object? product)
        {
            if (product is ProductoTiendaViewModel prod)
            {
                var cartItem = new CarritoItemViewModel
                {
                    IdProducto = prod.IdProducto,
                    NombreProducto = prod.Nombre,
                    ImagenProducto = prod.Imagen,
                    PrecioUnitario = prod.PrecioVenta,
                    Cantidad = 1
                };

                _cartViewModel.AddItem(cartItem);
                UpdateCartCount();

                MessageBox.Show($"'{prod.Nombre}' agregado al carrito",
                              "Producto Agregado",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
        }

        private void ViewCart()
        {
            NavigateToCart?.Invoke();
        }

        private void UpdateCartCount()
        {
            CartItemCount = _cartViewModel.TotalItems;
        }

        private async void LoadProductsFromDatabase()
        {
            try
            {
                var productos = await _productoService.ObtenerProductosActivosAsync();

                AllProducts.Clear();
                foreach (var producto in productos)
                {
                    AllProducts.Add(producto);
                }

                // Cargar todos los productos inicialmente
                Search();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }
    }
}
