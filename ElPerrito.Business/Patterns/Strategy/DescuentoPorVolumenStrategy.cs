namespace ElPerrito.Business.Patterns.Strategy
{
    public class DescuentoPorVolumenStrategy : IDescuentoStrategy
    {
        private readonly int _cantidadMinima;
        private readonly decimal _porcentajeDescuento;

        public DescuentoPorVolumenStrategy(int cantidadMinima, decimal porcentajeDescuento)
        {
            _cantidadMinima = cantidadMinima;
            _porcentajeDescuento = porcentajeDescuento;
        }

        public decimal CalcularDescuento(decimal montoOriginal)
        {
            // Este método requeriría cantidad de items, simplificamos
            return montoOriginal * (_porcentajeDescuento / 100m);
        }

        public string ObtenerDescripcion()
        {
            return $"Descuento del {_porcentajeDescuento}% por compra de {_cantidadMinima}+ unidades";
        }
    }
}
