namespace ElPerrito.Business.Patterns.Composite
{
    /// <summary>
    /// Patrón Composite - Componente base para estructura jerárquica de categorías
    /// </summary>
    public interface IMenuComponent
    {
        string GetNombre();
        int GetId();
        void MostrarInformacion(int nivel = 0);
    }
}
