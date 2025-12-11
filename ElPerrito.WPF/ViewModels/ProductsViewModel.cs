using ElPerrito.WPF.Commands;
using ElPerrito.WPF.Models;
using ElPerrito.WPF.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ElPerrito.WPF.ViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        private string _searchText = string.Empty;
        private ProductoViewModel? _selectedProduct;
        private string _selectedCategory = "Todas";
        private bool _showActiveOnly = true;
        private readonly ProductoService _productoService;

        public ProductsViewModel()
        {
            _productoService = new ProductoService();
            Products = new ObservableCollection<ProductoViewModel>();
            AllProducts = new ObservableCollection<ProductoViewModel>();
            Categories = new ObservableCollection<string> { "Todas", "Alimentos", "Medicamentos", "Accesorios", "Juguetes" };

            // Comandos
            SearchCommand = new RelayCommand(_ => Search());
            AddProductCommand = new RelayCommand(_ => AddProduct());
            EditProductCommand = new RelayCommand(product => EditProduct(product));
            DeleteProductCommand = new RelayCommand(product => DeleteProduct(product));

            // Cargar productos desde la base de datos
            LoadProductsFromDatabase();
        }

        public ObservableCollection<ProductoViewModel> Products { get; }
        public ObservableCollection<ProductoViewModel> AllProducts { get; }
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

        public ProductoViewModel? SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
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

        public bool ShowActiveOnly
        {
            get => _showActiveOnly;
            set
            {
                SetProperty(ref _showActiveOnly, value);
                Search();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

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
                    p.Nombre.ToLower().Contains(SearchText.ToLower()));
            }

            // Filtrar por estado activo
            if (ShowActiveOnly)
            {
                filtered = filtered.Where(p => p.Activo);
            }

            foreach (var product in filtered)
            {
                Products.Add(product);
            }
        }

        private void AddProduct()
        {
            // TODO: Abrir ventana de agregar producto
        }

        private void EditProduct(object? product)
        {
            // TODO: Abrir ventana de editar producto
        }

        private void DeleteProduct(object? product)
        {
            // TODO: Implementar eliminación de producto
        }

        private async void LoadProductsFromDatabase()
        {
            try
            {
                var productos = await _productoService.ObtenerTodosLosProductosAsync();

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
