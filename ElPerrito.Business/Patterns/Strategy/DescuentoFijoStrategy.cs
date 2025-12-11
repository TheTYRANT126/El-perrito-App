using System;

namespace ElPerrito.Business.Patterns.Strategy
{
    public class DescuentoFijoStrategy : IDescuentoStrategy
    {
        private readonly decimal _montoFijo;

        public DescuentoFijoStrategy(decimal montoFijo)
        {
            if (montoFijo < 0)
                throw new ArgumentException("El monto fijo no puede ser negativo");

            _montoFijo = montoFijo;
        }

        public decimal CalcularDescuento(decimal montoOriginal)
        {
            return Math.Min(_montoFijo, montoOriginal);
        }

        public string ObtenerDescripcion()
        {
            return $"Descuento fijo de ${_montoFijo:F2}";
        }
    }
}
