using ElPerrito.Data.Context;
using ElPerrito.WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ElPerrito.WPF.Services
{
    public class ProductoService
    {
        private readonly ElPerritoContext _context;
        private readonly string _assetsPath;

        public ProductoService()
        {
            _context = new ElPerritoContext();
            // Obtener ruta base del ejecutable
            _assetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Images");
        }

        public async Task<List<ProductoTiendaViewModel>> ObtenerProductosActivosAsync()
        {
            try
            {
                // Primero cargar los datos de la base de datos
                var productosDb = await _context.Productos
                    .Include(p => p.IdCategoriaNavigation)
                    .Include(p => p.Inventario)
                    .Where(p => p.Activo == true)
                    .ToListAsync();

                // Luego procesar las imágenes en memoria
                var productos = productosDb.Select(p => new ProductoTiendaViewModel
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion ?? "Sin descripción",
                    Categoria = p.IdCategoriaNavigation.Nombre,
                    PrecioVenta = p.PrecioVenta,
                    Stock = p.Inventario != null ? p.Inventario.Stock : 0,
                    Imagen = ObtenerRutaImagen(p.IdProducto, p.Imagen),
                    Activo = p.Activo == true
                }).ToList();

                return productos;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al cargar productos: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}",
                                              "Error",
                                              System.Windows.MessageBoxButton.OK,
                                              System.Windows.MessageBoxImage.Error);
                return new List<ProductoTiendaViewModel>();
            }
        }

        public async Task<List<ProductoViewModel>> ObtenerTodosLosProductosAsync()
        {
            try
            {
                // Cargar todos los productos (activos e inactivos) para admin
                var productosDb = await _context.Productos
                    .Include(p => p.IdCategoriaNavigation)
                    .Include(p => p.Inventario)
                    .OrderByDescending(p => p.IdProducto)
                    .ToListAsync();

                var productos = productosDb.Select(p => new ProductoViewModel
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Categoria = p.IdCategoriaNavigation.Nombre,
                    PrecioVenta = p.PrecioVenta,
                    Stock = p.Inventario != null ? p.Inventario.Stock : 0,
                    Activo = p.Activo == true
                }).ToList();

                return productos;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al cargar productos: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}",
                                              "Error",
                                              System.Windows.MessageBoxButton.OK,
                                              System.Windows.MessageBoxImage.Error);
                return new List<ProductoViewModel>();
            }
        }

        private string ObtenerRutaImagen(int idProducto, string? imagenDb)
        {
            // Si la imagen viene con el formato "ID_product/imagen.ext"
            if (!string.IsNullOrEmpty(imagenDb) && imagenDb.Contains("_product"))
            {
                var rutaCompleta = Path.Combine(_assetsPath, imagenDb);
                if (File.Exists(rutaCompleta))
                {
                    return rutaCompleta;
                }
            }

            // Buscar en la carpeta {id}_product
            var carpetaProducto = Path.Combine(_assetsPath, $"{idProducto}_product");
            if (Directory.Exists(carpetaProducto))
            {
                var imagenes = Directory.GetFiles(carpetaProducto, "*.*")
                    .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                               f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                               f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                               f.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                if (imagenes.Length > 0)
                {
                    return imagenes[0]; // Retorna la primera imagen encontrada
                }
            }

            // Retornar placeholder si no se encontró imagen
            var placeholderPath = Path.Combine(_assetsPath, "placeholder.png");
            return File.Exists(placeholderPath) ? placeholderPath : "";
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
