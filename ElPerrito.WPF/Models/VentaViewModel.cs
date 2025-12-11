using System;

namespace ElPerrito.WPF.Models
{
    public class VentaViewModel
    {
        public int IdVenta { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string EstadoPago { get; set; } = string.Empty;
    }
}
