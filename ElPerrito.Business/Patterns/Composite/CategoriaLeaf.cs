using System;

namespace ElPerrito.Business.Patterns.Composite
{
    public class CategoriaLeaf : IMenuComponent
    {
        private readonly int _id;
        private readonly string _nombre;
        private readonly string _descripcion;

        public CategoriaLeaf(int id, string nombre, string descripcion)
        {
            _id = id;
            _nombre = nombre;
            _descripcion = descripcion;
        }

        public string GetNombre() => _nombre;
        public int GetId() => _id;

        public void MostrarInformacion(int nivel = 0)
        {
            string indent = new string(' ', nivel * 2);
            Console.WriteLine($"{indent}└─ {_nombre} (ID: {_id}) - {_descripcion}");
        }
    }
}
