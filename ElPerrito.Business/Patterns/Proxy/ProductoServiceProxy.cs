using ElPerrito.Data.Entities;
using ElPerrito.Core.Logging;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Proxy
{
    /// <summary>
    /// Patrón Proxy - Añade caché y logging al servicio real
    /// </summary>
    public class ProductoServiceProxy : IProductoService
    {
        private readonly IProductoService _realService;
        private readonly IMemoryCache _cache;
        private readonly Logger _logger = Logger.Instance;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public ProductoServiceProxy(IProductoService realService, IMemoryCache cache)
        {
            _realService = realService;
            _cache = cache;
        }

        public async Task<Producto?> ObtenerProductoAsync(int id)
        {
            string cacheKey = $"producto_{id}";

            if (_cache.TryGetValue(cacheKey, out Producto? producto))
            {
                _logger.LogInfo($"Producto {id} obtenido del caché");
                return producto;
            }

            _logger.LogInfo($"Producto {id} no en caché, consultando BD");
            producto = await _realService.ObtenerProductoAsync(id);

            if (producto != null)
            {
                _cache.Set(cacheKey, producto, _cacheDuration);
            }

            return producto;
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            string cacheKey = "productos_todos";

            if (_cache.TryGetValue(cacheKey, out List<Producto>? productos))
            {
                _logger.LogInfo("Productos obtenidos del caché");
                return productos!;
            }

            _logger.LogInfo("Productos no en caché, consultando BD");
            productos = await _realService.ObtenerTodosAsync();

            _cache.Set(cacheKey, productos, _cacheDuration);

            return productos;
        }
    }
}
