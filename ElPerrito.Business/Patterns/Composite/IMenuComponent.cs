namespace ElPerrito.Business.Patterns.Composite
{
    public interface IMenuComponent
    {
        string GetNombre();
        int GetId();
        void MostrarInformacion(int nivel = 0);
    }
}
