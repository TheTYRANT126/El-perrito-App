using System.Collections.Generic;

namespace ElPerrito.Business.Patterns.Observer
{
    public class StockAlertData
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
    }

    public class StockSubject : ISubject<StockAlertData>
    {
        private readonly List<IObserver<StockAlertData>> _observers = new();

        public void Attach(IObserver<StockAlertData> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Detach(IObserver<StockAlertData> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(StockAlertData data)
        {
            foreach (var observer in _observers)
            {
                observer.Update(data);
            }
        }

        public void CheckStock(int idProducto, string nombreProducto, int stockActual, int stockMinimo)
        {
            if (stockActual <= stockMinimo)
            {
                Notify(new StockAlertData
                {
                    IdProducto = idProducto,
                    NombreProducto = nombreProducto,
                    StockActual = stockActual,
                    StockMinimo = stockMinimo
                });
            }
        }
    }
}
