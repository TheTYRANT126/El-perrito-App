using System;

namespace ElPerrito.Business.Patterns.Strategy
{
    public class DescuentoPorcentajeStrategy : IDescuentoStrategy
    {
        private readonly decimal _porcentaje;

        public DescuentoPorcentajeStrategy(decimal porcentaje)
        {
            if (porcentaje < 0 || porcentaje > 100)
                throw new ArgumentException("El porcentaje debe estar entre 0 y 100");

            _porcentaje = porcentaje;
        }

        public decimal CalcularDescuento(decimal montoOriginal)
        {
            return montoOriginal * (_porcentaje / 100m);
        }

        public string ObtenerDescripcion()
        {
            return $"Descuento del {_porcentaje}%";
        }
    }
}
