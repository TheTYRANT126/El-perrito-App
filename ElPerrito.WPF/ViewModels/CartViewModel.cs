using ElPerrito.WPF.Commands;
using ElPerrito.WPF.Models;
using ElPerrito.WPF.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ElPerrito.WPF.ViewModels
{
    public class CartViewModel : ViewModelBase
    {
        private readonly CarritoService _carritoService;
        private int _totalItems;
        private decimal _totalAmount;
        private bool _isCartEmpty = true;

        public CartViewModel()
        {
            _carritoService = new CarritoService();
            CartItems = new ObservableCollection<CarritoItemViewModel>();

            // Comandos
            IncreaseQuantityCommand = new RelayCommand(item => IncreaseQuantity(item));
            DecreaseQuantityCommand = new RelayCommand(item => DecreaseQuantity(item));
            RemoveFromCartCommand = new RelayCommand(item => RemoveFromCart(item));
            ClearCartCommand = new RelayCommand(async _ => await ClearCart());
            CheckoutCommand = new RelayCommand(async _ => await Checkout(), _ => !IsCartEmpty && CurrentSession.Instance.IsCliente);
            BackToShopCommand = new RelayCommand(_ => BackToShop());

            // Actualizar totales
            UpdateTotals();
        }

        public ObservableCollection<CarritoItemViewModel> CartItems { get; }

        public int TotalItems
        {
            get => _totalItems;
            set => SetProperty(ref _totalItems, value);
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            set => SetProperty(ref _totalAmount, value);
        }

        public bool IsCartEmpty
        {
            get => _isCartEmpty;
            set => SetProperty(ref _isCartEmpty, value);
        }

        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseQuantityCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand ClearCartCommand { get; }
        public ICommand CheckoutCommand { get; }
        public ICommand BackToShopCommand { get; }

        public event System.Action? NavigateToShop;

        public async System.Threading.Tasks.Task LoadCartFromDatabase()
        {
            if (!CurrentSession.Instance.IsCliente)
                return;

            try
            {
                var items = await _carritoService.CargarCarritoCliente(CurrentSession.Instance.UserId);
                CartItems.Clear();
                foreach (var item in items)
                {
                    CartItems.Add(item);
                }
                UpdateTotals();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar el carrito: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        public async System.Threading.Tasks.Task SaveCartToDatabase()
        {
            if (!CurrentSession.Instance.IsCliente)
                return;

            try
            {
                await _carritoService.GuardarCarritoCliente(CurrentSession.Instance.UserId, CartItems.ToList());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al guardar el carrito: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        public async void AddItem(CarritoItemViewModel newItem)
        {
            // Verificar si el producto ya existe en el carrito
            var existingItem = CartItems.FirstOrDefault(i => i.IdProducto == newItem.IdProducto);

            if (existingItem != null)
            {
                // Incrementar cantidad si ya existe
                existingItem.Cantidad++;
            }
            else
            {
                // Agregar nuevo item
                CartItems.Add(newItem);
            }

            UpdateTotals();

            // Guardar en BD si es cliente autenticado
            if (CurrentSession.Instance.IsCliente)
            {
                await SaveCartToDatabase();
            }
        }

        public void ClearCartLocal()
        {
            CartItems.Clear();
            UpdateTotals();
        }

        private async void IncreaseQuantity(object? item)
        {
            if (item is CarritoItemViewModel cartItem)
            {
                cartItem.Cantidad++;
                UpdateTotals();

                // Guardar en BD si es cliente autenticado
                if (CurrentSession.Instance.IsCliente)
                {
                    await SaveCartToDatabase();
                }
            }
        }

        private async void DecreaseQuantity(object? item)
        {
            if (item is CarritoItemViewModel cartItem)
            {
                if (cartItem.Cantidad > 1)
                {
                    cartItem.Cantidad--;
                    UpdateTotals();

                    // Guardar en BD si es cliente autenticado
                    if (CurrentSession.Instance.IsCliente)
                    {
                        await SaveCartToDatabase();
                    }
                }
                else
                {
                    // Si la cantidad es 1, preguntar si desea eliminar
                    var result = MessageBox.Show($"¿Desea eliminar '{cartItem.NombreProducto}' del carrito?",
                                                "Eliminar Producto",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        RemoveFromCart(item);
                    }
                }
            }
        }

        private async void RemoveFromCart(object? item)
        {
            if (item is CarritoItemViewModel cartItem)
            {
                CartItems.Remove(cartItem);
                UpdateTotals();

                // Guardar en BD si es cliente autenticado
                if (CurrentSession.Instance.IsCliente)
                {
                    await SaveCartToDatabase();
                }

                MessageBox.Show($"'{cartItem.NombreProducto}' eliminado del carrito",
                              "Producto Eliminado",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
        }

        private async System.Threading.Tasks.Task ClearCart()
        {
            if (CartItems.Count == 0)
                return;

            var result = MessageBox.Show("¿Está seguro de vaciar el carrito?",
                                       "Vaciar Carrito",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                CartItems.Clear();
                UpdateTotals();

                // Vaciar en BD si es cliente autenticado
                if (CurrentSession.Instance.IsCliente)
                {
                    await _carritoService.VaciarCarritoCliente(CurrentSession.Instance.UserId);
                }

                MessageBox.Show("Carrito vaciado correctamente",
                              "Carrito Vacío",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
        }

        private async System.Threading.Tasks.Task Checkout()
        {
            if (CartItems.Count == 0)
                return;

            if (!CurrentSession.Instance.IsCliente)
            {
                MessageBox.Show("Debe iniciar sesión como cliente para finalizar la compra",
                              "Iniciar Sesión",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"¿Confirmar compra por un total de {TotalAmount:C2}?",
                                       "Finalizar Compra",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Crear la venta en la base de datos
                    var idVenta = await _carritoService.CrearVentaDesdeCarrito(
                        CurrentSession.Instance.UserId,
                        CartItems.ToList()
                    );

                    MessageBox.Show($"¡Compra realizada con éxito!\n\nNúmero de venta: {idVenta}\nTotal: {TotalAmount:C2}\nProductos: {TotalItems}",
                                  "Compra Exitosa",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);

                    CartItems.Clear();
                    UpdateTotals();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error al procesar la compra: {ex.Message}",
                                  "Error",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Error);
                }
            }
        }

        private void BackToShop()
        {
            NavigateToShop?.Invoke();
        }

        private void UpdateTotals()
        {
            TotalItems = CartItems.Sum(i => i.Cantidad);
            TotalAmount = CartItems.Sum(i => i.Subtotal);
            IsCartEmpty = CartItems.Count == 0;

            // Notificar cambio en el comando Checkout
            OnPropertyChanged(nameof(CheckoutCommand));
        }
    }
}
