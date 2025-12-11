using ElPerrito.Data.Entities;
using System.Collections.Generic;

namespace ElPerrito.Business.Patterns.Iterator
{
    public class ProductoIterator : IIterator<Producto>
    {
        private readonly List<Producto> _productos;
        private int _currentIndex = 0;

        public ProductoIterator(List<Producto> productos)
        {
            _productos = productos;
        }

        public bool HasNext()
        {
            return _currentIndex < _productos.Count;
        }

        public Producto Next()
        {
            return _productos[_currentIndex++];
        }

        public void Reset()
        {
            _currentIndex = 0;
        }
    }

    public class ActiveProductoIterator : IIterator<Producto>
    {
        private readonly List<Producto> _productos;
        private int _currentIndex = 0;

        public ActiveProductoIterator(List<Producto> productos)
        {
            _productos = productos.FindAll(p => p.Activo == true);
        }

        public bool HasNext()
        {
            return _currentIndex < _productos.Count;
        }

        public Producto Next()
        {
            return _productos[_currentIndex++];
        }

        public void Reset()
        {
            _currentIndex = 0;
        }
    }
}
