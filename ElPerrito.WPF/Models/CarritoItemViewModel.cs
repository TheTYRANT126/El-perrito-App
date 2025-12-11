using ElPerrito.WPF.ViewModels;

namespace ElPerrito.WPF.Models
{
    public class CarritoItemViewModel : ViewModelBase
    {
        private int _cantidad;

        public int IdItem { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string ImagenProducto { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }

        public int Cantidad
        {
            get => _cantidad;
            set
            {
                if (SetProperty(ref _cantidad, value))
                {
                    // Notificar que el subtotal también cambió
                    OnPropertyChanged(nameof(Subtotal));
                }
            }
        }

        public decimal Subtotal => PrecioUnitario * Cantidad;
    }
}
