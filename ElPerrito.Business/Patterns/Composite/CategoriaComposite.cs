using System;
using System.Collections.Generic;

namespace ElPerrito.Business.Patterns.Composite
{
    /// <summary>
    /// Composite - Categoría que puede contener subcategorías
    /// </summary>
    public class CategoriaComposite : IMenuComponent
    {
        private readonly int _id;
        private readonly string _nombre;
        private readonly List<IMenuComponent> _hijos = new();

        public CategoriaComposite(int id, string nombre)
        {
            _id = id;
            _nombre = nombre;
        }

        public void Agregar(IMenuComponent componente)
        {
            _hijos.Add(componente);
        }

        public void Remover(IMenuComponent componente)
        {
            _hijos.Remove(componente);
        }

        public string GetNombre() => _nombre;
        public int GetId() => _id;

        public void MostrarInformacion(int nivel = 0)
        {
            string indent = new string(' ', nivel * 2);
            Console.WriteLine($"{indent}┌─ {_nombre} (ID: {_id})");

            foreach (var hijo in _hijos)
            {
                hijo.MostrarInformacion(nivel + 1);
            }
        }
    }
}
